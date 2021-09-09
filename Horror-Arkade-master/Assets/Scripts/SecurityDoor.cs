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
    [SerializeField] GameObject disabledLock;

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
            
            if (doorLight)
                doorLight.color = _red;
            
            closedDoor.SetActive(true);
            openedDoor.SetActive(false);
        } else if (isOpen)
        {
            isLocked = false;
            
            if (doorLight)
                doorLight.color = _green;

            if (disabledLock)
                disabledLock.SetActive(false);
            
            openedDoor.SetActive(true);
            closedDoor.SetActive(false);
        }else if (!isOpen && !isLocked)
        {
            isLocked = false;
            
            if (doorLight)
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
