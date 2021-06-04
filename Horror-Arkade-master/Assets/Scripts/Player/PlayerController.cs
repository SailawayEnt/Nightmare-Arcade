using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Sub Behaviours")]
    // public PlayerMovementBehaviour playerMovementBehaviour;
    // public PlayerAnimationBehaviour playerAnimationBehaviour;
    
    [Header("Input Settings")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] float movementSmoothingSpeed = 1f;
    Vector3 rawInputMovement;
    Vector3 smoothInputMovement;
     
     //Action Maps
     string actionMapPlayerControls = "Main Player Controls";
     string actionMapMenuControls = "Menu Controls";

     //Current Control Scheme
     string currentControlScheme;
     
     //This is called from the GameManager; when the game is being setup.
     public void SetupPlayer(int newPlayerID)
     {
         currentControlScheme = playerInput.currentControlScheme;

         // playerMovementBehaviour.SetupBehaviour();
         // playerAnimationBehaviour.SetupBehaviour();
     }
}
