using System;
using UnityEngine;

public abstract class InventoryItem : ScriptableObject
{
    const int DefaultStack = 0;
    
    [Header("Basic Info")]
    [SerializeField] new string name = "New Inventory Item Name";
    [SerializeField] Sprite icon = null;

    [Header("Item Data")]
    [SerializeField][Min(1)]int maxStack = 1;
    
    int _currentStack = 0;

    public int CurrentStack
    {
        get { return _currentStack; }
        set
        { _currentStack = value; }
    }

    void OnEnable()
    {
        _currentStack = DefaultStack;
    }

    public string Name => name;

    public Sprite Icon => icon;
    public int MaxStack => maxStack;
}

