public interface  IItemContainer
{
    ItemSlot AddItem(ItemSlot itemSlot);
    void RemoveItem(ItemSlot itemSlot);
    bool HasItem(InventoryItem item);
    int GetTotalQuantity(InventoryItem item);
}
