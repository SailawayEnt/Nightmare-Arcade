using System;
using UnityEngine;

public class PlayerMovementBehaviour : PhysicsObject
{
    [Header("Component References")]
    [SerializeField] Transform playerTransform;

    [Header("Movement Settings")]
    public float movementSpeed = 3f;
    
    //Stored Values
    Vector3 movementDirection;

    public void MoveThePlayer()
    {
        
    }
}


