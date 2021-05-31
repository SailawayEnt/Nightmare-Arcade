using UnityEngine;

[CreateAssetMenu(fileName = "New Persistant Item", menuName = "Items/Persistant Item")]
public class PersistantItem : InventoryItem
{
    const bool  DefaultState = false;
    
    [Header("Persistant Data")] 
    [SerializeField] bool _hasReceived;
    
    public bool HasReceived
    {
        get => _hasReceived;
        set => _hasReceived = value;
    }

    void OnEnable()
    {
        _hasReceived = DefaultState;
    }
}