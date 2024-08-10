using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IKitchenObjectParent, IProgressable
{

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    private float fryingProgress = 0f;
    private FryingRecipeSO currentFryingRecipe;
    public event EventHandler<IProgressable.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Coocking
    }

    private State state;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (currentFryingRecipe != null)
        {
            fryingProgress += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs { progress = fryingProgress / currentFryingRecipe.fryingTimerMax });
            if (fryingProgress > currentFryingRecipe.fryingTimerMax)
            {
                currentKitchenObject.DestroySelf();
                KitchenObject.SpawnKitchenObject(currentFryingRecipe.output, this);
                FryingRecipeSO newFryingRecipe = GetCurrentRecipeWithInput(currentKitchenObject.GetKitchenObjectSO());
                ChangeCurrentRecipe(newFryingRecipe);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (currentKitchenObject == null && player.HasKitchenObject())
        {
            FryingRecipeSO newFryingRecipe = GetCurrentRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO());
            ChangeCurrentRecipe(newFryingRecipe);
            player.GetKitchenObject().SetParent(this);
        }
        else if (currentKitchenObject != null && !player.HasKitchenObject())
        {
            currentKitchenObject.SetParent(player);
            ChangeCurrentRecipe(null);
        }
    }

    private FryingRecipeSO GetCurrentRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipe in fryingRecipeSOArray)
        {
            if (fryingRecipe.input == kitchenObjectSO)
            {
                return fryingRecipe;
            }
        }

        return null;
    }

    private void ChangeCurrentRecipe(FryingRecipeSO newRecipe)
    {
        OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs { progress = 0 });
        fryingProgress = 0f;
        currentFryingRecipe = newRecipe;
        if (newRecipe != null)
        {
            state = State.Coocking;
        }
        else
        {
            state = State.Idle;
        }
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
    }
}
