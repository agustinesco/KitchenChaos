using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour
{



    [SerializeField]
    protected Transform counterTopPoint;
    protected KitchenObject currentKitchenObject;
    public virtual void Interact(Player player)
    {

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
