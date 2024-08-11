using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent, IProgressable
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public EventHandler OnCutPerformed;
    public event EventHandler<IProgressable.OnProgressChangedEventArgs> OnProgressChanged;

    private int cuttingProgress = 0;


    public override void Interact(Player player)
    {
        if (currentKitchenObject == null && player.HasKitchenObject())
        {
            player.GetKitchenObject().SetParent(this);
        }
        else if (currentKitchenObject != null)
        {
            if (player.GetKitchenObject() != null && player.GetKitchenObject().TryGetPlate(out PlateKitchenObject playerPlateKitchenObject))
            {
                if (playerPlateKitchenObject.TryAddingIngredient(currentKitchenObject.GetKitchenObjectSO()))
                {
                    currentKitchenObject.DestroySelf();
                    OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs { progress = 0 });
                    cuttingProgress = 0;
                }
            }
            else if (player.GetKitchenObject() == null)
            {
                currentKitchenObject.SetParent(player);
                OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs { progress = 0 });
                cuttingProgress = 0;
            }
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
                OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs { progress = (float)cuttingProgress / currentCuttingRecipe.cuttingMin });
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
