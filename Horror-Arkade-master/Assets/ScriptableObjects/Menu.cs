using UnityEngine;

public enum Type
{
    Main_Menu,
    Pause_Menu
}

[CreateAssetMenu(fileName = "NewMenu", menuName = "Scriptable Object/Scene Data/Menu")]
public class Menu : GameScene
{
    //Settings specific to menu only
    [Header("Menu specific")]
    public Type type;
}