using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent currentParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetParent(IKitchenObjectParent parent)
    {
        if (!parent.HasKitchenObject())
        {
            if (currentParent != null)
            {
                currentParent.SetKitchenObject(null);
            }
            currentParent = parent;
            currentParent.SetKitchenObject(this);
            transform.parent = currentParent.GetTopPoint();
            transform.localPosition = Vector3.zero;
        }
    }

    public IKitchenObjectParent GetParent()
    {
        return currentParent;
    }
}
