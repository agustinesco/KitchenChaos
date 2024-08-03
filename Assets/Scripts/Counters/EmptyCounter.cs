using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField]
    private Transform counterTopPoint;

    private KitchenObject currentKitchenObject;
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

    public Transform GetTopPoint()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.currentKitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return currentKitchenObject;
    }

    public bool HasKitchenObject()
    {
        return currentKitchenObject != null;
    }
}
