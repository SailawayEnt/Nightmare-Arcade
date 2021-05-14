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
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            var position = targetDestination.position;
            col.transform.position = new Vector2(position.x, position.y);
        }
    }
}
