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
            if (player.GetKitchenObject() != null && player.GetKitchenObject().TryGetPlate(out PlateKitchenObject playerPlateKitchenObject))
            {
                if (playerPlateKitchenObject.TryAddingIngredient(currentKitchenObject.GetKitchenObjectSO()))
                {
                    currentKitchenObject.DestroySelf();
                }
            }
            else if (player.GetKitchenObject() != null && currentKitchenObject.TryGetPlate(out PlateKitchenObject counterPlateKitchenObject))
            {
                if (counterPlateKitchenObject.TryAddingIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().DestroySelf();
                }

            }
            else if (player.GetKitchenObject() == null)
            {
                currentKitchenObject.SetParent(player);
            }
        }
    }
}
