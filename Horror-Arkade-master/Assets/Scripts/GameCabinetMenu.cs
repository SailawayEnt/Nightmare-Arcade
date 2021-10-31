using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCabinetMenu : MonoBehaviour
{
   [Header("Target")] [SerializeField] Transform targetDestination;

   [SerializeField] GameObject cabinetPlayer;
   [SerializeField] ScenesData scenesData;
   [SerializeField] GameObject startMenu;
   [SerializeField] GameObject continueMenu;

   [Header("Main Game References")] [SerializeField]
   GameObject player;

   [SerializeField] Vector2Value playerPositionStorage;

   GameObject _gameControllerGO;
   Transform _levelControllerTransform;


   public void StartGame()
   {
      _gameControllerGO = GameObject.Find("Game_Controller");
      _levelControllerTransform = _gameControllerGO.transform.Find("Level_Controller");
      HideStartMenu();
      StartCoroutine(WaitForLoad());
   }


   IEnumerator WaitForLoad()
   {
      _levelControllerTransform.gameObject.SetActive(true);
      yield return new WaitForSeconds(.25f);
      StartCoroutine(_levelControllerTransform.GetComponent<LevelController>().LoadFirstWave());
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
