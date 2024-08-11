using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour
{



    [SerializeField]
    protected Transform counterTopPoint;
    protected KitchenObject currentKitchenObject;
    public static event EventHandler OnObjectDrop;

    public virtual void Interact(Player player)
    {

        Debug.Log("Interact");
    }
    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("Interact Alternate");
    }

    public Transform GetTopPoint()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.currentKitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnObjectDrop?.Invoke(this, EventArgs.Empty);
        }
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
