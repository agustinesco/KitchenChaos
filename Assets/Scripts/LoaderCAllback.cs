using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCAllback : MonoBehaviour
{
    private bool isFirstupdate = true;
    private void Update()
    {
        if (isFirstupdate)
        {
            isFirstupdate = false;

            Loader.LoaderCallback();
        }
    }
}
