using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpdateInteractableUI : MonoBehaviour
{
    //Input System Stored Data
    private InputActionAsset focusedInputActionAsset;
    private PlayerInput focusedPlayerInput;
    private InputAction focusedInputAction;
    
    [Header("Device Display Settings")]
    public DeviceDisplayConfigurator deviceDisplaySettings;
    
    [Header("UI Display - Binding Text or Icon")]
    public TextMeshProUGUI bindingNameDisplayText;
    public Image bindingIconDisplayImage;
    
    public void UpdateDisplayUI()
    {
        Debug.Log("trying to update the UI");
        int controlBindingIndex = focusedInputAction.GetBindingIndexForControl(focusedInputAction.controls[0]);
        string currentBindingInput = InputControlPath.ToHumanReadableString(focusedInputAction.bindings[controlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        Sprite currentDisplayIcon = deviceDisplaySettings.GetDeviceBindingIcon(focusedPlayerInput, currentBindingInput);

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
