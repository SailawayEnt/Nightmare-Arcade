using UnityEditor;
using UnityEngine;

public static class CheatMenu
{
    [MenuItem("Cheats/Move Player To Start")]
    public static void MovePlayerToStart()
    {
        NewPlayer.Instance.gameObject.transform.position = new Vector2(-44.42f, -5.822211f);
    }
    
    [MenuItem("Cheats/Move Player Inside The Arkade")]
    public static void MovePlayerInsideArkade()
    {
        NewPlayer.Instance.gameObject.transform.position = new Vector2(56f, 18.5f);
    }
    
    [MenuItem("Cheats/Move Player Outside The Arkade")]
    public static void MovePlayerOutsideArkade()
    {
        NewPlayer.Instance.gameObject.transform.position = new Vector2(70f, -2.21f);
    }
}
