using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Windows : MonoBehaviour
{
    readonly Vector3 _closedAccentPosition = new Vector3(0.04000009f, 1.635f, 1);
    readonly Vector3 _openAccentPosition = new Vector3(0.04000009f, 2.766f, 1);
    
    readonly Color _red = new Color(.88f, .16f, 16f, 1);
    readonly Color _green = new Color(.31f,.95f,.18f, 1);
    
    [SerializeField] SpriteRenderer windowAccentLight;
    [SerializeField] bool isLocked;
    [SerializeField] bool isOpen;
    [SerializeField] GameObject openedWindow;
    [SerializeField] GameObject closedWindow;
    [SerializeField] GameObject movableAccent;
    [SerializeField] GameObject windowLight;
    

    void Awake()
    {
        WindowCheck();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            WindowCheck();
        }
    }

    void WindowCheck()
    {
        if (isLocked)
        {
            isOpen = false;
            
            if (windowAccentLight)
                windowAccentLight.color = _red;
            
            closedWindow.SetActive(true);
            openedWindow.SetActive(false);
            
            if (windowLight)
            {
                windowLight.SetActive(false);   
            }

            if (movableAccent)
                movableAccent.transform.localPosition = _closedAccentPosition;
        } else if (isOpen)
        {
            isLocked = false;
            
            if (windowAccentLight)
                windowAccentLight.color = _green;
            
            openedWindow.SetActive(true);
            closedWindow.SetActive(false);
            if (windowLight)
            {
                windowLight.SetActive(true);   
            }
            if (movableAccent)
                movableAccent.transform.localPosition = _openAccentPosition;
        }else if (!isOpen && !isLocked)
        {
            isLocked = false;
            
            if (windowAccentLight)
                windowAccentLight.color = _green;
            
            closedWindow.SetActive(true);
            openedWindow.SetActive(false);
            
            if (windowLight)
            {
                windowLight.SetActive(false);
            }
            if (movableAccent)
                movableAccent.transform.localPosition = _closedAccentPosition;
        }
        else
        {
            Debug.LogWarning("You cannot have a locked open door." + gameObject);
        }
    }
}
