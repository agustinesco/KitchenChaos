using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public EventHandler OnCutPerformed;
    public EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progress;
    }
    private int cuttingProgress = 0;


    public override void Interact(Player player)
    {
        if (currentKitchenObject == null && player.HasKitchenObject())
        {
            player.GetKitchenObject().SetParent(this);
        }
        else if (currentKitchenObject != null && !player.HasKitchenObject())
        {
            currentKitchenObject.SetParent(player);
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progress = 0 });
            cuttingProgress = 0;
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (currentKitchenObject != null)
        {
            CuttingRecipeSO currentCuttingRecipe = GetCurrentRecipeWithInput(currentKitchenObject.GetKitchenObjectSO());
            if (currentCuttingRecipe != null)
            {
                cuttingProgress++;
                OnCutPerformed?.Invoke(this, EventArgs.Empty);
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progress = (float)cuttingProgress / currentCuttingRecipe.cuttingMin });
                if (currentCuttingRecipe.cuttingMin <= cuttingProgress)
                {

                    currentKitchenObject.DestroySelf();

                    KitchenObject.SpawnKitchenObject(currentCuttingRecipe.output, this);
                }
            }
        }
    }

    private CuttingRecipeSO GetCurrentRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var cuttingRecipe in cuttingRecipeSOArray)
        {
            if (cuttingRecipe.input == kitchenObjectSO)
            {
                return cuttingRecipe;
            }
        }

        return null;
    }


}
