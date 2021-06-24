using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD : MonoBehaviour
{
    void Start()
    {
        ShowAd();
    }
    void Update()
    {

    }

    public void ShowAd()
    {
        Application.ExternalCall("ShowAd");
    }
}
