using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOnTrigger : MonoBehaviour
{
   Animator _animator;
   ParticleSystem _particleSystem;
   private static readonly int move = Animator.StringToHash("trashMove");

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        

    }

   private void OnTriggerEnter2D(Collider2D col)
   {
       if (col.gameObject == NewPlayer.Instance.gameObject)
       {
           _animator.SetTrigger(move);
           _particleSystem.Play();
       }
   }
}
