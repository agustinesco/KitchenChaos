using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisuals : MonoBehaviour
{

    [SerializeField] CuttingCounter cuttingCounter;
    private Animator animator;
    private const string CUT = "Cut";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        cuttingCounter.OnCutPerformed += CuttingCounter_OnCutPerformed;
    }

    private void CuttingCounter_OnCutPerformed(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
