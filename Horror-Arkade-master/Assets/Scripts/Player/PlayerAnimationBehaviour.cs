using UnityEngine;

public class PlayerAnimationBehaviour : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] Animator playerAnimator;

    //Animation String IDs
    int _playerVelocityXAnimationID;
    int _playerVelocityYAnimationID;
    int _playerAttackAnimationID;
    int _playerGroundedAnimationID;
    int _playerHurtAnimationID;
    int _playerMoveDirectionAnimationID;
    int _playerAttackDirectionYAnimationID;
    int _playerFlashAnimationID;
    int _playerShootingAnimationID;
    int _playerPoundedAnimationID;

    void Awake()
    {
        SetupAnimationIDs();
    }

    void SetupAnimationIDs()
    {
        _playerAttackAnimationID = Animator.StringToHash("attack");
        _playerVelocityXAnimationID = Animator.StringToHash("velocityX");
        _playerVelocityYAnimationID = Animator.StringToHash("velocityY");
        _playerGroundedAnimationID = Animator.StringToHash("grounded");
        _playerHurtAnimationID = Animator.StringToHash("hurt");
        _playerMoveDirectionAnimationID = Animator.StringToHash("moveDirection");
        _playerAttackDirectionYAnimationID = Animator.StringToHash("attackDirectionY");
        _playerFlashAnimationID = Animator.StringToHash("flash");
        _playerShootingAnimationID = Animator.StringToHash("shooting");
        _playerPoundedAnimationID = Animator.StringToHash("pounded");
    }

    public void UpdateMovementAnimation(float velocityX, float velocityY, int moveDirection)
    {
        playerAnimator.SetFloat(_playerVelocityXAnimationID, velocityX);
        playerAnimator.SetFloat(_playerVelocityYAnimationID, velocityY);
        playerAnimator.SetInteger(_playerMoveDirectionAnimationID, moveDirection);
    }

    public void UpdateAttackDirection(int attackDirection)
    {
        playerAnimator.SetFloat(_playerAttackDirectionYAnimationID, attackDirection);
    }

    public void PlayAttackAnimation()
    {
        playerAnimator.SetTrigger(_playerAttackAnimationID);
    }

    public void ResetAttackAnimation()
    {
        playerAnimator.ResetTrigger(_playerAttackAnimationID);
    }
    
    public void SetShootingAnimation(bool isShooting)
    {
        playerAnimator.SetBool(_playerShootingAnimationID, isShooting);
    }
    
    public void SetPoundedAnimationValue(bool isPounded)
    {
        playerAnimator.SetBool(_playerPoundedAnimationID, isPounded);
    }

    public void SetGroundedAnimationValue(bool isGrounded)
    {
        playerAnimator.SetBool(_playerGroundedAnimationID, isGrounded);
    }

    public bool GetGroundedAnimationValue()
    {
        return playerAnimator.GetBool(_playerGroundedAnimationID);
    }
    
    public void SetHurtAnimationValue()
    {
        playerAnimator.SetTrigger(_playerHurtAnimationID);
    }
    
    public void SetFlashAnimationValue()
    {
        playerAnimator.SetTrigger(_playerFlashAnimationID);
    }
}
