using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{

    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;
    [SerializeField]
    private Transform counterTopPoint;

    private KitchenObject currentKitchenObject;
    public override void Interact(Player player)
    {
        Debug.Log("llega interact");
        if (!player.HasKitchenObject())
        {
            Debug.Log("entra if");

            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetParent(player);
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
