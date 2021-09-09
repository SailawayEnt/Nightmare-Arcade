using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityDoor : MonoBehaviour
{
    [SerializeField] SpriteRenderer doorLight;
    [SerializeField] bool isLocked;
    [SerializeField] bool isOpen;
    [SerializeField] GameObject openedDoor;
    [SerializeField] GameObject closedDoor;

    Color _red = new Color(.88f, .16f, 16f, 1);
    Color _green = new Color(.31f,.95f,.18f, 1);

    void Awake()
    {
        DoorCheck();
    }

    void DoorCheck()
    {
        if (isLocked)
        {
            isOpen = false;
            doorLight.color = _red;
            closedDoor.SetActive(true);
            openedDoor.SetActive(false);
        } else if (isOpen)
        {
            isLocked = false;
            doorLight.color = _green;
            openedDoor.SetActive(true);
            closedDoor.SetActive(false);
        }else if (!isOpen && !isLocked)
        {
            isLocked = false;
            doorLight.color = _green;
            closedDoor.SetActive(true);
            openedDoor.SetActive(false);
        }
        else
        {
            Debug.LogWarning("You cannot have a locked open door." + gameObject);
        }
    }
}
