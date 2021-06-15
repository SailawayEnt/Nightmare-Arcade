using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

public class EventSystemManager : Singleton<EventSystemManager>
{
    [Header("Component References")]
    public EventSystem eventSystem;
    public InputSystemUIInputModule inputSystemUIInputModule;

    void Awake()
    {
        DontDestroyChildOnLoad(gameObject);
    }

    
    public void SetCurrentSelectedGameObject(GameObject newSelectedGameObject)
    {
        eventSystem.SetSelectedGameObject(newSelectedGameObject);
        Button newSelectable = newSelectedGameObject.GetComponent<Button>();
        newSelectable.Select();
        newSelectable.OnSelect(null);
    }

    // public void UpdateActionAssetToFocusedPlayer()
    // {
    //     PlayerController focusedPlayerController = GameManager.Instance.GetFocusedPlayerController();
    //     inputSystemUIInputModule.actionsAsset = focusedPlayerController.GetActionAsset();
    // }

    public InputActionAsset GetInputActionAsset()
    {
        return inputSystemUIInputModule.actionsAsset;
    }

}