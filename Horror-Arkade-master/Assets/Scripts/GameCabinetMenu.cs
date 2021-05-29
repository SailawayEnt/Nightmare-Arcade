﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCabinetMenu : MonoBehaviour
{
   [Header("Target")] 
   [SerializeField] Transform targetDestination;

   // Player cabinetPlayer;
   [SerializeField] CabinetPlayer cabinetPlayer;
   [SerializeField] ScenesData scenesData;
   public GameObject menu;
   // [SerializeField] GameEvent onRecievedTicket;
   // public ConsumableItem ticketInventory;

   GameObject levelControllerGO;

   void Awake()
   {
      cabinetPlayer = GameObject.FindObjectOfType<CabinetPlayer>();
   }

   public void StartGame()
   {
      HideMenu();
      var position = targetDestination.position;
      cabinetPlayer.transform.position = new Vector2(position.x, position.y);
      StartCoroutine(WaitForLoad());
   }


   IEnumerator WaitForLoad()
   {
      yield return new WaitForSeconds(0.25f);
      levelControllerGO = GameObject.Find("Level_Controller");
      StartCoroutine(levelControllerGO.GetComponent<LevelController>().LoadFirstWave());
      
      
   }
   // public void CollectTicket()
   // {
   //    if (ticketInventory.CurrentStack < ticketInventory.MaxStack)
   //    {
   //       ticketInventory.CurrentStack += 1;
   //       onRecievedTicket?.Invoke();
   //    }
   //    scenesData.LoadLevelWithIndex(1);
   // }
   
   public void ExitCabinet()
   {
      scenesData.LoadLevelWithIndex(1);
   }
   
   void HideMenu()
   {
      menu.SetActive(false);
   }
    
}
