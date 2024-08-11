using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject progressable;
    [SerializeField] private Image barImage;

    public void Start()
    {
        IProgressable progressableComponent = progressable.GetComponent<IProgressable>();
        if (progressableComponent == null)
        {
            throw new Exception($"Component {progressable} has no deffinition for IProgress interface");
        }
        progressableComponent.OnProgressChanged += Progressable_OnProgressChanged;
        Hide();

    }

    private void Progressable_OnProgressChanged(object sender, IProgressable.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progress;
        if (e.progress == 0f || e.progress == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show() { gameObject.SetActive(true); }
    private void Hide() { gameObject.SetActive(false); }
}