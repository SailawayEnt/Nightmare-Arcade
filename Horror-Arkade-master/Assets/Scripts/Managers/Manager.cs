﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
   public static Manager Instance;
   private void Awake()
   {
      if (Instance != null)
      {
         Destroy(gameObject);
      }

      Instance = this;
      
   }
}
