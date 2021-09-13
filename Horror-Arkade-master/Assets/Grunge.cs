using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunge : MonoBehaviour
{
    [SerializeField] GameObject grunge;
    [SerializeField] bool enableGrunge = true;

    private void Awake()
    {
        if (grunge != null)
        {
            grunge.SetActive(enableGrunge);
        }
    }
}
