using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCounter : BaseCounter, IKitchenObjectParent
{
    public override void Interact(Player player)
    {
        if (currentKitchenObject == null && player.HasKitchenObject())
        {
            player.GetKitchenObject().SetParent(this);
        }
        else if (currentKitchenObject != null)
        {
            if (player.HasKitchenObject())
            {

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject playerPlateKitchenObject))
                {
                    if (playerPlateKitchenObject.TryAddingIngredient(currentKitchenObject.GetKitchenObjectSO()))
                    {
                        currentKitchenObject.DestroySelf();
                    }
                }
                else if (currentKitchenObject.TryGetPlate(out PlateKitchenObject counterPlateKitchenObject))
                {
                    if (counterPlateKitchenObject.TryAddingIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().DestroySelf();
                    }

                }
            }
            else
            {
                currentKitchenObject.SetParent(player);
            }
        }
    }
}
