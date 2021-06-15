using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sceneDB", menuName = "Scriptable Object/Scene Data/Database")]
public class ScenesData : ScriptableObject
{
    public List<Levels> levels = new List<Levels>();
    public List<Menu> menus = new List<Menu>();
    public int CurrentLevelIndex = 1;
    public Vector2Value startingPosition;

    /*
     * Levels
     */


    //Load a scene with a given index
    public void LoadLevelWithIndex(int index)
    {
        if (index <= levels.Count)
        {
            SceneControlManager.LoadScene(index, 1, 2);
            //Load Gameplay scene for the level
            // SceneManager.LoadSceneAsync("Gameplay" + index.ToString());
            //Load first part of the level in additive mode
            // SceneManager.LoadSceneAsync("Level" + index.ToString() + "Part1", LoadSceneMode.Additive);
        }

        //reset the index if we have no more levels
        else
        {
            CurrentLevelIndex =1;
        }
    }
    //Start next level
    public void NextLevel()
    {
        CurrentLevelIndex++;
        LoadLevelWithIndex(CurrentLevelIndex);
    }
    //Restart current level
    public void RestartLevel()
    {
        LoadLevelWithIndex(CurrentLevelIndex);
    }
    //New game, load level 1
    public void NewGame()
    {
        startingPosition.initialValue = new Vector2(-44.42f, -5.822211f);
        LoadLevelWithIndex(1);
    }
   
    /*
     * Menus
     */

    //Load main Menu
    public void LoadMainMenu()
    {
        // SceneManager.LoadSceneAsync(menus[(int)Type.Main_Menu].sceneName);
    }
    //Load Pause Menu
    public void LoadPauseMenu()
    {
        // SceneManager.LoadSceneAsync(menus[(int)Type.Pause_Menu].sceneName);
    }
}