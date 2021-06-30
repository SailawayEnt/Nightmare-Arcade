using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Input Device Value", menuName = "Scriptable Object/Data Types/Input Device")]
public class InputDeviceValue : ScriptableObject
{
    const InputDevice DefaultValue = null;
    
    InputDevice _initialValue;

    public InputDevice InitialValue
    {
        get => _initialValue;
        set => _initialValue = value;
    }
    
    void OnEnable()
    {
        _initialValue = DefaultValue;
    }
}