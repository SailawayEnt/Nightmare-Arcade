using UnityEngine;

[CreateAssetMenu(fileName = "New String Value", menuName = "Scriptable Object/Data Types/String")]
public class StringValue : ScriptableObject
{
    const string DefaultValue = null;
    
    string _initialValue;

    public string InitialValue
    {
        get => _initialValue;
        set => _initialValue = value;
    }
    
    void OnEnable()
    {
        _initialValue = DefaultValue;
    }
}