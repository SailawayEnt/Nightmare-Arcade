using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Scene Data/Level")]
public class Levels : GameScene
{
    //Settings specific to level only
    [Header("Level specific")]
    public string levelNickname;
}