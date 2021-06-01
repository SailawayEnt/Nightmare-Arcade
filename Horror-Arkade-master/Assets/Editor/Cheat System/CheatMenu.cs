using Packages.Rider.Editor.UnitTesting;
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
    
    [MenuItem("Cheats/Move Player Left Of Bridge")]
    public static void MovePlayerLeftOfBridge()
    {
        NewPlayer.Instance.gameObject.transform.position = new Vector2(115.3f, -7.102964f);
    }

    [MenuItem("Cheats/Collect 100 Credits")]
    public static void Collect100Credits()
    {
        NewPlayer.Instance.coins += 100;
    }
    
    [MenuItem("Cheats/Collect 1,000 Credits")]
    public static void Collect1000Credits()
    {
        NewPlayer.Instance.coins += 1000;
    }
    
    [MenuItem("Cheats/Collect 10,000 Credits")]
    public static void Collect10000Credits()
    {
        NewPlayer.Instance.coins += 10000;
    }
    
    [MenuItem("Cheats/Collect 1 Ticket")]
    public static void Collect1Ticket()
    {
        CheatManager.Instance.Receive1Ticket();
    }
}
