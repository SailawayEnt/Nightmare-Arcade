using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class GameCabinetMenu : MonoBehaviour
{
   [Header("Target")] [SerializeField] Transform targetDestination;

   //[SerializeField] GameObject menuCamera;
   [SerializeField] GameObject playingCamera;

   // Player cabinetPlayer;
   [SerializeField] GameObject cabinetPlayer;
   [SerializeField] ScenesData scenesData;
   [SerializeField] GameObject startMenu;
   [SerializeField] GameObject continueMenu;

   [Header("Main Game References")] [SerializeField]
   GameObject player;

   [SerializeField] Vector2Value playerPositionStorage;


   GameObject _levelControllerGO;

   public void StartGame()
   {
      HideStartMenu();
      playingCamera.SetActive(true);
      var position = targetDestination.position;
      cabinetPlayer.transform.position = new Vector2(position.x, position.y);
      StartCoroutine(WaitForLoad());
   }


   IEnumerator WaitForLoad()
   {
      yield return new WaitForSeconds(.25f);
      _levelControllerGO = GameObject.Find("Level_Controller");
      _levelControllerGO.SetActive(true);
      StartCoroutine(_levelControllerGO.GetComponent<LevelController>().LoadFirstWave());
   }

   public void ExitCabinet()
   {
      cabinetPlayer.SetActive(false);
      player.SetActive(true);
      scenesData.LoadLevelWithIndex(2);
   }

   void HideStartMenu()
   {
      startMenu.SetActive(false);
   }
   
   public void HideContinueMenu()
   {
      continueMenu.SetActive(false);
   }

   public void ResetCabinetGame()
   {
      playingCamera.SetActive(false);
      cabinetPlayer.transform.position = new Vector2(0, 0);

      RemoveLeftOverGameObjects();

      ShowContinueMenu();

   }

   void RemoveLeftOverGameObjects()
   {
      var resettableGOList = new List<Resettable>(FindObjectsOfType<Resettable>());

      if (resettableGOList.Count <= 0) return;

      foreach (var resettableObject in resettableGOList)
      {
         Destroy(resettableObject.gameObject);
      }
      
      resettableGOList.Clear();
      
   }

   void ShowContinueMenu()
   {
      continueMenu.SetActive(true);
   }

}
