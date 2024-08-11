using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSo_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSo_GameObject> kitchenObjectSo_GameObjects;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSo_GameObject kitchenObjectSo_GameObject in kitchenObjectSo_GameObjects)
        {
            if (kitchenObjectSo_GameObject.kitchenObjectSO == e.ingredient)
            {
                kitchenObjectSo_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
