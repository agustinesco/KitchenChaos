using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisuals : MonoBehaviour
{

    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private const string OPEN_CLOSE = "OpenClose";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        containerCounter.OnPlayerGrabObject += containerCounter_OnPlayerGrabObject;
    }

    private void containerCounter_OnPlayerGrabObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
