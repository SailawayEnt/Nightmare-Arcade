using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Scriptable Object/Items/Consumable Item")]
public class ConsumableItem : InventoryItem
{
    [Header("Consumable Data")] [SerializeField]
    private string useText = "Do something, Maybe?";

}
