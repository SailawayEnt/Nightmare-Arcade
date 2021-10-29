using UnityEngine;

public class CheckingForTicket : MonoBehaviour
{
    [SerializeField] ConsumableItem coinInventory;
    [SerializeField] GameCabinetMenu gameManager;
    
    bool HasCoin()
    {
        return coinInventory.CurrentStack > 0;
    }

    public void StartGame()
    {
        if (HasCoin())
        {
            //todo: play coin insert sound
            coinInventory.CurrentStack--;
            gameManager.HideContinueMenu();
            gameManager.StartGame();
        }
        else
        {
            //todo: play cannot load sound
            gameManager.HideContinueMenu();
            gameManager.ExitCabinet();
        }
    }
}
