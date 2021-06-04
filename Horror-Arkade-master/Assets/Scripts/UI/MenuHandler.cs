using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Allows buttons to fire various functions, like QuitGame and LoadScene*/

public class MenuHandler : MonoBehaviour {
    
    public GameObject menu;
    // public GameObject loadingInterface;
    // public Image loadingProgressBar;
    //List of the scenes to load from Main Menu
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    public void StartGame()
    {
        Debug.Log("start is triggered");
        HideMenu();
        // ShowLoadingScreen();
        //Load the Scene asynchronously in the background
        // scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
        //Additive mode adds the Scene to the current loaded Scenes, in this case Gameplay scene
        // scenesToLoad.Add(SceneManager.LoadSceneAsync("Level01Part01", LoadSceneMode.Additive));
        // StartCoroutine(LoadingScreen());
    }

    public void StartGameSO()
    {
        HideMenu();
        // ShowLoadingScreen();
        //Load the Scene asynchronously in the background
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
        //Additive mode adds the Scene to the current loaded Scenes, in this case Gameplay scene
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Level01Part01", LoadSceneMode.Additive));
        // StartCoroutine(LoadingScreen());
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    // public void ShowLoadingScreen()
    // {
    //     loadingInterface.SetActive(true);
    // }

    // IEnumerator LoadingScreen()
    // {
    //     float totalProgress=0;
    //     //Iterate through all the scenes to load
    //     for(int i=0; i<scenesToLoad.Count; ++i)
    //     {
    //         while (!scenesToLoad[i].isDone)
    //         {
    //             //Adding the scene progress to the total progress
    //             totalProgress += scenesToLoad[i].progress;
    //             //the fillAmount needs a value between 0 and 1, so we devide the progress by the number of scenes to load
    //             loadingProgressBar.fillAmount = totalProgress/scenesToLoad.Count;
    //             yield return null;
    //         }
    //     }
    // }
    
}
