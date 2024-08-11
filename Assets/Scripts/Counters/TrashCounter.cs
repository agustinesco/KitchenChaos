using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjectTrash;

    new public static void ResetStaticData()
    {
        OnObjectTrash = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnObjectTrash?.Invoke(this, EventArgs.Empty);
        }
    }
}
