using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpdateInteractableUI : MonoBehaviour
{
    //Input System Stored Data
    // InputActionAsset focusedInputActionAsset;
    PlayerInput _playerInput;
    InputAction _inputAction;
    NewPlayer _player;
    [SerializeField] StringValue savedBindingInput;
    [SerializeField] SpriteValue savedSpriteValue;

    [Header("Rebind Settings")]
    public string actionName;
    
    [Header("Device Display Settings")]
    public DeviceDisplayConfigurator deviceDisplaySettings;
    
    [Header("UI Display - Binding Text or Icon")]
    public TextMeshProUGUI bindingNameDisplayText;
    public Image bindingIconDisplayImage;

    void Start()
    {
        if (savedBindingInput.InitialValue == null || savedSpriteValue.InitialValue == null)
        {
            UpdateBehaviour();
        }
        else
        {
            Sprite currentDisplayIcon = savedSpriteValue.InitialValue;
        
            if(currentDisplayIcon)
            {
                ToggleGameObjectState(bindingNameDisplayText.gameObject, false);
                ToggleGameObjectState(bindingIconDisplayImage.gameObject, true);
                bindingIconDisplayImage.sprite = currentDisplayIcon;
        
            } else if(currentDisplayIcon == null)
            {
                ToggleGameObjectState(bindingNameDisplayText.gameObject, true);
                ToggleGameObjectState(bindingIconDisplayImage.gameObject, false);
                bindingNameDisplayText.SetText(savedBindingInput.InitialValue);
            }
        }
    }

    public void UpdateBehaviour()
    {
        GetPlayerInput();
        SetupInputAction();
        // UpdateActionDisplayUI();
        UpdateDisplayUI();
    }
    
    void GetPlayerInput()
    {
        _player = FindObjectOfType<NewPlayer>();
        _playerInput = _player.GetPlayerInput();
    }
    
    void SetupInputAction()
    {
        _inputAction = _playerInput.actions.FindAction(actionName);
    }

    public void UpdateDisplayUI()
    {
        int controlBindingIndex = _inputAction.GetBindingIndexForControl(_inputAction.controls[0]);
        
        string currentBindingInput = InputControlPath.ToHumanReadableString(_inputAction.bindings[controlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        savedBindingInput.InitialValue = currentBindingInput;

        Sprite currentDisplayIcon = deviceDisplaySettings.GetDeviceBindingIcon(_playerInput, currentBindingInput);
        savedSpriteValue.InitialValue = currentDisplayIcon;

        if(currentDisplayIcon)
        {
            ToggleGameObjectState(bindingNameDisplayText.gameObject, false);
            ToggleGameObjectState(bindingIconDisplayImage.gameObject, true);
            bindingIconDisplayImage.sprite = currentDisplayIcon;

        } else if(currentDisplayIcon == null)
        {
            ToggleGameObjectState(bindingNameDisplayText.gameObject, true);
            ToggleGameObjectState(bindingIconDisplayImage.gameObject, false);
            bindingNameDisplayText.SetText(currentBindingInput);
        }
    }
    
    void ToggleGameObjectState(GameObject targetGameObject, bool newState)
    {
        targetGameObject.SetActive(newState);
    }
}
