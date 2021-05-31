using UnityEngine;

public class CheatManager : MonoBehaviour
{
    [Header("Reference")] 
    [SerializeField] GameEvent onTicketReceived;
    [SerializeField] ConsumableItem ticketInventorySystem;
    
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

    public void Receive1Ticket()
    {
        if (ticketInventorySystem.CurrentStack < ticketInventorySystem.MaxStack)
        {
            ticketInventorySystem.CurrentStack += 1;
            onTicketReceived.Invoke();
        }
    }
    
}
