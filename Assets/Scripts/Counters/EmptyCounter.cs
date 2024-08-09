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
        else if (currentKitchenObject != null && !player.HasKitchenObject())
        {
            currentKitchenObject.SetParent(player);
        }
    }
}
