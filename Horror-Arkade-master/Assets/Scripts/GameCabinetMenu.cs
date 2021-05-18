using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCabinetMenu : MonoBehaviour
{

   [SerializeField] private ScenesData _scenesData;

   public void CollectTicket()
   {
      Debug.Log("Collecting Ticket");
   }
   
   public void ExitCabinet()
   {
      _scenesData.LoadLevelWithIndex(1);
   }
    
}
