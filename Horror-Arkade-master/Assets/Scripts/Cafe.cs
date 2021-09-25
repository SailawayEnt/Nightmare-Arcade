using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

[ExecuteAlways]
public class Cafe : MonoBehaviour
{
    [SerializeField] bool isLightsOff;
    [SerializeField] GameObject blackOverlay;
    void Awake()
    {
        ChangeLightStatus();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            ChangeLightStatus();
        }
    }

    void ChangeLightStatus()
    {
        if (blackOverlay != null)
        {
            blackOverlay.SetActive(isLightsOff);
        }
    }
}
