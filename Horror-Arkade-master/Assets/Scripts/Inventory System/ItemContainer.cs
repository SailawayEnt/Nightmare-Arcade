using System;

[Serializable]
public class ItemContainer : IItemContainer
{
    private ItemSlot[] _itemSlots = new ItemSlot[0];
    
    public Action OnItemsUpdated = delegate {  };
    // public GameEvent inventoryEvent;

    public ItemContainer(int size) => _itemSlots = new ItemSlot[size]; 
    
    public ItemSlot GetSlotByIndex(int index) => _itemSlots[index];

    public ItemSlot AddItem(ItemSlot itemSlot)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].item != null )
            {
                if (_itemSlots[i].item == itemSlot.item)
                {
                    int slotRemainingSpace = _itemSlots[i].item.MaxStack - _itemSlots[i].quantity;

                    if (_itemSlots[i].quantity <= slotRemainingSpace)
                    {
                        _itemSlots[i].quantity += itemSlot.quantity;

                        itemSlot.quantity = 0;
                        
                        // inventoryEvent.Invoke();
                        OnItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else if (slotRemainingSpace > 0)
                    {
                        _itemSlots[i].quantity += slotRemainingSpace;

                        itemSlot.quantity -= slotRemainingSpace;
                    }
                }
            }
        }
        for (int i = 0; i < itemSlot.item.MaxStack; i++)
        {
            if (_itemSlots[i].item == null)
            {
                if (itemSlot.quantity <= itemSlot.item.MaxStack)
                {
                    _itemSlots[i] = itemSlot;
            
                    itemSlot.quantity = 0;
            
                    // inventoryEvent.Invoke();
                    OnItemsUpdated.Invoke();
                    
                    return itemSlot;
                }
                else
                {
                    _itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);
                    
                    itemSlot.quantity -= itemSlot.item.MaxStack;
                }
            }
        }
        
        // inventoryEvent.Invoke();
        OnItemsUpdated.Invoke();
        
        return itemSlot;
    }

    public void RemoveItem(ItemSlot itemSlot)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].item != null)
            {
                if (_itemSlots[i].item == itemSlot.item)
                {
                    if (_itemSlots[i].quantity < itemSlot.quantity)
                    {
                        itemSlot.quantity -= _itemSlots[i].quantity;

                        _itemSlots[i] = new ItemSlot();
                    }
                    else
                    {
                        _itemSlots[1].quantity -= itemSlot.quantity;

                        if (_itemSlots[i].quantity == 0)
                        {
                            _itemSlots[i] = new ItemSlot();
                            
                            // inventoryEvent.Invoke();
                            OnItemsUpdated.Invoke();
                            
                            return;
                        }
                    }
                }
            }
        }
    }

    public bool HasItem(InventoryItem item)
    {
        foreach (ItemSlot itemSlot in _itemSlots)
        {
            if (itemSlot.item == null) { continue; }
            if (itemSlot.item != item) { continue; }
            
            return true;
        }
        return false;
    }

    public int GetTotalQuantity(InventoryItem item)
    {
        int totalCount = 0;

        foreach (ItemSlot itemSlot in _itemSlots)
        {
            if (itemSlot.item == null) { continue; }
            if (itemSlot.item != item) { continue; }

            totalCount += itemSlot.quantity;

        }

        return totalCount;
    }
}
