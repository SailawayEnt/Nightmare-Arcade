using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class InventorySlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ConsumableItem consumableItem;
    [SerializeField] TextMeshProUGUI itemQuantity = null;
    [SerializeField] Animator animator;
    float _item;
    float _itemEased;

    void Awake()
    {
        _item = consumableItem.CurrentStack;
        _itemEased = _item;
    }
    
    void Update()
    {
        // Update item text mesh to reflect how many coins the player has! However, we want them to count up.
        UpdateSlotUI();
    }

    public void UpdateSlotUI()
    {
            itemQuantity.text = Mathf.Round(_itemEased).ToString();
            _itemEased += ((float)consumableItem.CurrentStack - _itemEased) * Time.deltaTime * 5f;
            
            if (_itemEased >= _item)
            {
                animator.SetTrigger("getGem");
                _item = _itemEased + 1;
            }
    }
}