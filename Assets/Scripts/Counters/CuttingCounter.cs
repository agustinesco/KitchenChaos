using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if (currentKitchenObject == null && player.HasKitchenObject())
        {
            player.GetKitchenObject().SetParent(this);
        }
        else if (currentKitchenObject != null && !player.HasKitchenObject())
        {
            currentKitchenObject.SetParent(player);
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (currentKitchenObject != null)
        {
            KitchenObjectSO toCreateKitchenObjectSO = GetRecipeOutput(currentKitchenObject.GetKitchenObjectSO());
            if (toCreateKitchenObjectSO != null)
            {
                currentKitchenObject.DestroySelf();

                KitchenObject.SpawnKitchenObject(toCreateKitchenObjectSO, this);
            }
        }
    }

    private KitchenObjectSO GetRecipeOutput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var cuttingRecipe in cuttingRecipeSOArray)
        {
            if (cuttingRecipe.input == kitchenObjectSO)
            {
                return cuttingRecipe.output;
            }
        }

        return null;
    }


}
