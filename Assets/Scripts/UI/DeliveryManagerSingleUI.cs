using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform IconContainer;
    [SerializeField] private Transform IconTemplate;


    public void SetRecipe(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.name;
        foreach (Transform child in IconContainer)
        {
            if (child != IconTemplate)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(IconTemplate, IconContainer);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
            iconTransform.gameObject.SetActive(true);
        }
    }
}
