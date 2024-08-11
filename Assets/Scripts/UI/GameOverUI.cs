using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI recipesDeliveredText;


    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += KitchenGameManager_OnStateChange;
        Hide();
    }

    private void KitchenGameManager_OnStateChange(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.GetGameState() == KitchenGameManager.State.GameOver)
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetDeliveredRecipesAmount().ToString();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

}
