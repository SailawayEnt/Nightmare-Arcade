using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Scriptable Objects/Items/Consumable Item")]
public class ConsumableItem : InventoryItem
{
    [Header("Consumable Data")] [SerializeField]
    private string useText = "Do something, Maybe?";

}
