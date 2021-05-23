using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] ConsumableItem consumableItem;
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;

    void Awake()
    {
        itemQuantityText.text = consumableItem.CurrentStack.ToString();
    }

    public void UpdateSlotUI()
    {
            itemQuantityText.text = consumableItem.CurrentStack.ToString();
    }
    
}