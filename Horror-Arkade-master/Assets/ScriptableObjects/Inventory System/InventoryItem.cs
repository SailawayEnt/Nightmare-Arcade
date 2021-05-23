using System;
using UnityEngine;

public abstract class InventoryItem : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] new string name = "New Inventory Item Name";
    [SerializeField] Sprite icon = null;

    [Header("Item Data")]
    [SerializeField][Min(1)]int maxStack = 1;
    
    [Min(0)]public int defaultStack = 0;
    int _currentStack = 0;

    public int CurrentStack
    {
        get { return _currentStack; }
        set { _currentStack = value; }
    }

    void OnEnable()
    {
        _currentStack = defaultStack;
    }

    public string Name => name;

    public Sprite Icon => icon;
    public int MaxStack => maxStack;
}

