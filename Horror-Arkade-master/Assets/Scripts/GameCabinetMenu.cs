using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCabinetMenu : MonoBehaviour
{
   [Header("Target")] 
   [SerializeField] Transform targetDestination;
   //[SerializeField] GameObject menuCamera;
   [SerializeField] GameObject playingCamera;
   
   // Player cabinetPlayer;
   [SerializeField] GameObject cabinetPlayer;
   [SerializeField] ScenesData scenesData;
   public GameObject menu;

   [Header("Main Game References")] 
   [SerializeField] GameObject player;
   [SerializeField] Vector2Value playerPositionStorage;
   

   GameObject _levelControllerGO;

   void Awake()
   {
      
   }


   public void StartGame()
   {
      HideMenu();
      playingCamera.SetActive(true);
      var position = targetDestination.position;
      cabinetPlayer.transform.position = new Vector2(position.x, position.y);
      StartCoroutine(WaitForLoad());
   }


   IEnumerator WaitForLoad()
   {
      yield return new WaitForSeconds(.25f);
      _levelControllerGO = GameObject.Find("Level_Controller");
      StartCoroutine(_levelControllerGO.GetComponent<LevelController>().LoadFirstWave());
   }
   
   public void ExitCabinet()
   {
      cabinetPlayer.SetActive(false);
      player.SetActive(true);
      scenesData.LoadLevelWithIndex(2);
   }
   
   void HideMenu()
   {
      menu.SetActive(false);
   }

   public void ResetCabinetGame()
   {
      playingCamera.SetActive(false);
      cabinetPlayer.transform.position = new Vector2(0, 0);

      ShowMenu();
   }

   void ShowMenu()
   {
      menu.SetActive(true);
   }
    
}
