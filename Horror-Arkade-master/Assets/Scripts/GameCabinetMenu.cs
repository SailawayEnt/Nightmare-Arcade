using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCabinetMenu : MonoBehaviour
{
   [Header("Target")] 
   [SerializeField] Transform targetDestination;
   
   // Player cabinetPlayer;
   [SerializeField] GameObject cabinetPlayer;
   [SerializeField] ScenesData scenesData;
   public GameObject menu;
   // [SerializeField] GameEvent onRecievedTicket;
   // public ConsumableItem ticketInventory;

   [Header("Main Game References")] 
   [SerializeField] GameObject player;
   [SerializeField] Vector2Value playerPositionStorage;
   

   GameObject _levelControllerGO;
   

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
      _levelControllerGO = GameObject.Find("Level_Controller");
      StartCoroutine(_levelControllerGO.GetComponent<LevelController>().LoadFirstWave());
      
      
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
      cabinetPlayer.SetActive(false);
      player.SetActive(true);
      scenesData.LoadLevelWithIndex(1);
   }
   
   void HideMenu()
   {
      menu.SetActive(false);
   }

   public void ResetCabinetGame()
   {
      cabinetPlayer.transform.position = new Vector2(0, 0);

      ShowMenu();
   }

   void ShowMenu()
   {
      menu.SetActive(true);
   }
    
}
