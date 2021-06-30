using UnityEngine;

[CreateAssetMenu(fileName = "New Sprite Value", menuName = "Scriptable Object/Data Types/Sprite")]
public class SpriteValue : ScriptableObject
{
    const Sprite DefaultValue = null;
    
    Sprite _initialValue = null;

    public Sprite InitialValue
    {
        get => _initialValue;
        set => _initialValue = value;
    }
    
    void OnEnable()
    {
        _initialValue = DefaultValue;
    }

}