using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeListSO;


    private List<RecipeSO> waitingRecipeList = new List<RecipeSO>();
    private float spawnRecipeTimer = 0f;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeListMax = 4;
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnRecipeChange;
    public event EventHandler OnRecipeDelivered;
    public event EventHandler OnRecipeFailed;

    private int deliveredRecipesAmount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer > spawnRecipeTimerMax)
        {
            spawnRecipeTimer = 0f;
            if (waitingRecipeList.Count < waitingRecipeListMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.availableRecipes[UnityEngine.Random.Range(0, recipeListSO.availableRecipes.Count)];
                waitingRecipeList.Add(waitingRecipeSO);
                OnRecipeChange?.Invoke(this, EventArgs.Empty);

            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (RecipeSO waitingRecipeSO in waitingRecipeList)
        {
            if (waitingRecipeSO.kitchenObjectSOList.All(kitchenObjectSO => plateKitchenObject.GetKitchenObjectSOList().Contains(kitchenObjectSO)))
            {
                waitingRecipeList.Remove(waitingRecipeSO);
                OnRecipeChange?.Invoke(this, EventArgs.Empty);
                OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                deliveredRecipesAmount++;
                return;
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeList()
    {
        return waitingRecipeList;
    }

    public int GetDeliveredRecipesAmount()
    {
        return deliveredRecipesAmount;
    }
}
