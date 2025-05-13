using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFog : MonoBehaviour
{
    private bool fogOriginal;

    void OnPreRender()
    {
        fogOriginal = RenderSettings.fog;
        RenderSettings.fog = false;
    }

    void OnPostRender()
    {
        RenderSettings.fog = fogOriginal;
    }
}
