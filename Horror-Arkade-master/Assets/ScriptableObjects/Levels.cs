using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Scriptable Object/Scene Data/Level")]
public class Levels : GameScene
{
    //Settings specific to level only
    [Header("Level specific")]
    public string levelNickname;
}