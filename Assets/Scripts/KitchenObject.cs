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

    public void DestroySelf()
    {
        if (currentParent != null)
        {
            currentParent.SetKitchenObject(null);
        }
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, kitchenObjectParent.GetTopPoint());
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObject.SetParent(kitchenObjectParent);

        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }

        plateKitchenObject = null;
        return false;
    }

}
