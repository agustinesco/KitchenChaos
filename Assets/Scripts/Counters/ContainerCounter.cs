using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{

    public event EventHandler OnPlayerGrabObject;

    [SerializeField]
    protected KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {

            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetParent(player);

            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
    }


}
