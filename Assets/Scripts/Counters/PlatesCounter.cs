using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlatePickUp;

    private float spawnPlateTimerMax = 4f;
    private float spawnPlateTimer = 0f;

    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() && platesSpawnedAmount > 0)
        {

            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            platesSpawnedAmount--;
            OnPlatePickUp?.Invoke(this, EventArgs.Empty);

        }
    }
}
