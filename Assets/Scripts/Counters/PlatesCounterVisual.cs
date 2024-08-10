using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private float plateOffsetY = 0.1f;

    private List<GameObject> plateVisualGameObjectList = new List<GameObject>();
    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlatePickUp += PlatesCounter_OnPlatePickUp;
    }

    private void PlatesCounter_OnPlatePickUp(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        Destroy(plateGameObject);
        plateVisualGameObjectList.RemoveAt(plateVisualGameObjectList.Count - 1);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        plateVisualTransform.localPosition = new Vector3(0, plateVisualGameObjectList.Count * plateOffsetY, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
