using UnityEngine;

public class CheatManager : MonoBehaviour
{
    [Header("Reference")] 
    [SerializeField] GameEvent onTicketReceived;
    [SerializeField] ConsumableItem ticketInventorySystem;
    [SerializeField] GameEvent onCoinReceived;
    [SerializeField] ConsumableItem coinInventorSystem;
    
    // Singleton instantiation
    private static CheatManager _instance;
    public static CheatManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CheatManager>();
            }
            return _instance;
        }
    }

    public void ReceiveCoin(int quantity)
    {
        if (coinInventorSystem.CurrentStack < coinInventorSystem.MaxStack)
        {
            coinInventorSystem.CurrentStack += quantity;
            onCoinReceived.Invoke();
        }
    }
    public void ReceiveTicket(int quantity)
    {
        if (ticketInventorySystem.CurrentStack < ticketInventorySystem.MaxStack)
        {
            ticketInventorySystem.CurrentStack += quantity;
            onTicketReceived.Invoke();
        }
    }
    
}
