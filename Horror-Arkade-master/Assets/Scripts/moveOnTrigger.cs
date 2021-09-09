using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOnTrigger : MonoBehaviour
{
   [SerializeField] Animator animator;
   [SerializeField] ParticleSystem particleSystem;
   private static readonly int move = Animator.StringToHash("trashMove");

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        

    }

   private void OnTriggerEnter2D(Collider2D col)
   {
       if (col.gameObject == NewPlayer.Instance.gameObject)
       {
           animator.SetTrigger(move);
           particleSystem.Play();
       }
   }
}
