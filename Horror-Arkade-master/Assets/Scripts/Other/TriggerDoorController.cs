using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] Animator myDoor = null;

    [SerializeField] bool openTrigger = false;
    [SerializeField] bool closeTrigger = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);
                
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        
        
        if (other.CompareTag("Player"))
        {
            if (closeTrigger)
            {
                myDoor.Play("DoorClose", 0, 0.0f);
            }
        }
    }
}
