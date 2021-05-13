using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPortal : MonoBehaviour
{
    
    [Header("Target")]
    [SerializeField] private Transform targetDestination;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject && NewPlayer.Instance.grounded)
        {
            // col.transform.position = targetDestination.transform.position;
            col.transform.position = new Vector2(targetDestination.position.x, targetDestination.position.y);
        }
    }
}
