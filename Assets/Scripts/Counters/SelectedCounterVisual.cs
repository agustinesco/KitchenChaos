using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField]
    private BaseCounter baseCounter;
    [SerializeField]
    private GameObject visualGameObject;

    void Start()
    {
        Player.Intance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        visualGameObject.SetActive(e.selectedCounter == baseCounter);
    }
}
