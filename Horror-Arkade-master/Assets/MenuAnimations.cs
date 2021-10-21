using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MenuAnimations : MonoBehaviour
{
    public int HorizontalDirection { get; private set;  }
    
    public int VerticalDirection { get; private set;  }
    
    [SerializeField] Animator leftHandAnim;
    [SerializeField] Animator rightHandAnim;
    
    // Start is called before the first frame update
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        var newInputMovement = Vector2Int.RoundToInt(inputMovement);
        //_rawInputMovement = new Vector2(inputMovement.x, inputMovement.y);
        //_rawInputX = _rawInputMovement.x;
        
        HorizontalDirection = newInputMovement.x;
        
        VerticalDirection = newInputMovement.y;

        if (HorizontalDirection != 0)
        {
            leftHandAnim.SetInteger("moveDirection", HorizontalDirection);
        }
        else
        {
            leftHandAnim.SetInteger("moveDirection", 0);
        }

        if (VerticalDirection != 0)
        {
            leftHandAnim.SetInteger("vertDirection", VerticalDirection);
        }
        else
        {
            leftHandAnim.SetInteger("vertDirection", 0);
        }
        
        
    }

    public void OnSubmit()
    {
        rightHandAnim.SetTrigger("fire");
    }
}
