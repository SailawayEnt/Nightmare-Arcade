using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    [SerializeField] GameEvent onPlayerDeath;
    public GameObject destructionFX;

    public static Player Instance; 
    
    //Current Control Scheme
    string _currentControlScheme;
    
    //Animations
    
    
    [Header("Input Settings")]
    [SerializeField] PlayerInput playerInput;

    Vector2 _inputMovement;
    public Vector2 InputMovement { get; private set; }
    
    int _horizontalDirection;
    public int HorizontalDirection { get; private set;  }
    
    
    //INPUT SYSTEM ACTION METHODS --------------

    //This is called from PlayerInput; when a joystick or arrow keys has been pushed.
    //It stores the input Vector as a Vector3 to then be used by the smoothing function.


    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        var newInputMovement = Vector2Int.RoundToInt(inputMovement);
        InputMovement = value.ReadValue<Vector2>();
        
        HorizontalDirection = newInputMovement.x;
    }
    
    //INPUT SYSTEM AUTOMATIC CALLBACKS --------------

    //This is automatically called from PlayerInput, when the input device has changed
    //(IE: Keyboard -> Xbox Controller)
    public void OnControlsChanged()
    {

        if(playerInput.currentControlScheme != _currentControlScheme)
        {
            _currentControlScheme = playerInput.currentControlScheme;
            
            Debug.Log("TODO: OnControlsChanged");

            // playerVisualsBehaviour.UpdatePlayerVisuals();
            // RemoveAllBindingOverrides();
        }
    }
    
    //This is automatically called from PlayerInput, when the input device has been disconnected and can not be identified
    //IE: Device unplugged or has run out of batteries

    public void OnDeviceLost()
    {
        // playerVisualsBehaviour.SetDisconnectedDeviceVisuals();
        Debug.Log("TODO: OnDeviceLost");
    }


    public void OnDeviceRegained()
    {
        StartCoroutine(WaitForDeviceToBeRegained());
    }

    IEnumerator WaitForDeviceToBeRegained()
    {
        yield return new WaitForSeconds(0.1f);
        // playerVisualsBehaviour.UpdatePlayerVisuals();
        Debug.Log("TODO: WaitForDeviceToBeRegained");
    }

    void Awake()
    {
        if (Instance == null) 
            Instance = this;
    }

    void Start()
    {
        _currentControlScheme = playerInput.currentControlScheme;
    }

    //method for damage processing by 'Player'
    public void GetDamage(int damage)   
    {
        Destruction();
    }    

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
        onPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }
}