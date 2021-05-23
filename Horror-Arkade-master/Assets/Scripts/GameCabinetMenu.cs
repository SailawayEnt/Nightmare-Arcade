using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCabinetMenu : MonoBehaviour
{

   [SerializeField] private ScenesData _scenesData;
   [SerializeField] private GameEvent onRecievedTicket;
   public ConsumableItem ticketInventory;

   public void CollectTicket()
   {
      if (ticketInventory.CurrentStack < ticketInventory.MaxStack)
      {
         ticketInventory.CurrentStack += 1;
         onRecievedTicket.Invoke();
      }
      _scenesData.LoadLevelWithIndex(1);
   }
   
   public void ExitCabinet()
   {
      _scenesData.LoadLevelWithIndex(1);
   }
    
}
