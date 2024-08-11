using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += KitchenGameManager_OnStateChange;
        Hide();

    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStart()).ToString();
    }

    private void KitchenGameManager_OnStateChange(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.GetGameState() == KitchenGameManager.State.CountdownToStart)
        {
            Show();
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
