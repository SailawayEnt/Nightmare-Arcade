using System.Collections;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/*Adds player functionality to a physics object*/

[RequireComponent(typeof(RecoveryCounter))]

public class NewPlayer : PhysicsObject
{
    [Header("Sub Behaviours")]
    [SerializeField] PlayerAnimationBehaviour playerAnimationBehaviour;
    [SerializeField] PlayerMovementBehaviour playerMovementBehaviour;
    
    [Header("Input Settings")]
    [SerializeField] PlayerInput playerInput;
    float _rawInputX;
    Vector2 _rawInputMovement;
    int _horizontalDirection;
    public int HorizontalDirection { get; private set;  }
    
    int _verticalDirection;
    public int VerticalDirection { get; private set;  }
    
    float _interactPressedValue;
    public float InteractPressedValue { get; private set; }
    
    float _jumpPressedValue;
    public float JumpPressedValue { get; private set; }
    
    //Action Maps
    string actionMapPlayerControls = "Main Player Controls";
    string actionMapMenuControls = "Menu Controls";

    //Current Control Scheme
    string _currentControlScheme;
    
    [Header ("Reference")]
    public AudioSource audioSource;
    // public GameObject attackHit;
    CapsuleCollider2D capsuleCollider;
    public CameraEffects cameraEffects;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioSource flameParticlesAudioSource;
    [SerializeField] GameObject graphic;
    [SerializeField] Component[] graphicSprites;
    [SerializeField] ParticleSystem jumpParticles;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject machete;
    public RecoveryCounter recoveryCounter;
    

    // Singleton instantiation
    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    [Header("Properties")]
    [SerializeField] private string[] cheatItems;
    public bool dead = false;
    public bool frozen = false;
    private float fallForgivenessCounter; //Counts how long the player has fallen off a ledge
    [SerializeField] private float fallForgiveness = .2f; //How long the player can fall from a ledge and still jump
    [System.NonSerialized] public string groundType = "grass";
    [System.NonSerialized] public RaycastHit2D ground; 
    [SerializeField] Vector2 hurtLaunchPower; //How much force should be applied to the player when getting hurt?
    float launch; //The float added to x and y moveSpeed. This is set with hurtLaunchPower, and is always brought back to zero
    [SerializeField] private float launchRecovery; //How slow should recovering from the launch be? (Higher the number, the longer the launch will last)
    public float maxSpeed = 7; //Max move speed
    public float jumpPower = 17;
    private bool jumping;
    private Vector3 origLocalScale;
    [System.NonSerialized] public bool pounded;
    [System.NonSerialized] public bool pounding;
    [System.NonSerialized] public bool shooting = false;

    [Header ("Inventory")]
    public float ammo;
    public int coins;
    public int health;
    public int maxHealth;
    public int maxAmmo;
    [SerializeField] PersistantItem macheteData;
    [SerializeField] PersistantItem energyDrinkData;
    
    [Header ("Sounds")]
    public AudioClip deathSound;
    public AudioClip equipSound;
    public AudioClip grassSound;
    public AudioClip hurtSound;
    public AudioClip[] hurtSounds;
    public AudioClip holsterSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip poundSound;
    public AudioClip punchSound;
    public AudioClip[] poundActivationSounds;
    public AudioClip outOfAmmoSound;
    public AudioClip stepSound;
    [System.NonSerialized] public int whichHurtSound;
    

    void Start()
    {
        if (macheteData.HasReceived)
        {
            machete.SetActive(true);
        }
        
        _currentControlScheme = playerInput.currentControlScheme;
        
        Cursor.visible = false;
        SetUpCheatItems();
        health = maxHealth;
        origLocalScale = transform.localScale;
        recoveryCounter = GetComponent<RecoveryCounter>();


        //Find all sprites so we can hide them when the player dies.
        graphicSprites = GetComponentsInChildren<SpriteRenderer>();

        SetGroundType();
    }
    
    //INPUT SYSTEM ACTION METHODS --------------

    //This is called from PlayerInput; when a joystick or arrow keys has been pushed.
    //It stores the input Vector as a Vector3 to then be used by the smoothing function.


    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        var newInputMovement = Vector2Int.RoundToInt(inputMovement);
        _rawInputMovement = new Vector2(inputMovement.x, inputMovement.y);
        _rawInputX = _rawInputMovement.x;
        HorizontalDirection = newInputMovement.x;
        VerticalDirection = newInputMovement.y;
    }

    //This is called from PlayerInput, when a button has been pushed, that corresponds with the 'Attack' action
    public void OnAttack(InputAction.CallbackContext value)
    {
        if(macheteData.HasReceived && value.started && !frozen)
        {
            //Punch
            playerAnimationBehaviour.PlayAttackAnimation();
            Shoot(false);
        }
    }
    
    public void OnInteract(InputAction.CallbackContext value)
    {
        InteractPressedValue = value.ReadValue<float>();
    }
    
    //This is called from PlayerInput, when a button has been pushed, that corresponds with the 'Jump' action
    public void OnJump(InputAction.CallbackContext value)
    {
        JumpPressedValue = value.ReadValue<float>();
        if (value.started && playerAnimationBehaviour.GetGroundedAnimationValue() == true && !jumping && !frozen)
        {
            playerAnimationBehaviour.SetPoundedAnimationValue(false);
            Jump(1f);
        }
    }

    //This is called from Player Input, when a button has been pushed, that correspons with the 'TogglePause' action
    public void OnTogglePause(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            Debug.Log("TODO: Setup Pause");
            
            // pauseMenu.SetActive(true);
            
            // GameManager.Instance.TogglePauseState(this);
        }
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



    void Update()
    {
        ComputeVelocity();
    }

    protected void ComputeVelocity()
    {
        //Player movement
        Vector2 move = Vector2.zero;
        var playerTransform = transform;
        var position = playerTransform.position;
        ground = Physics2D.Raycast(new Vector2(position.x, position.y), -Vector2.up);

        //Lerp launch back to zero at all times
        launch += (0 - launch) * Time.deltaTime * launchRecovery;

        //Movement
        if (!frozen)
        {
            move.x = _rawInputX + launch;

            //Flip the graphic's localScale
            if (move.x > 0.01f)
            {
               graphic.transform.localScale = new Vector3(origLocalScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (move.x < -0.01f)
            {
               graphic.transform.localScale = new Vector3(-origLocalScale.x, transform.localScale.y, transform.localScale.z);
            }

            //
            // //Secondary attack (currently shooting) with right click
            // if (Input.GetMouseButtonDown(1))
            // {
            //     Shoot(true);
            // }
            // else if (Input.GetMouseButtonUp(1))
            // {
            //     Shoot(false);
            // }
            //
            // if (shooting)
            // {
            //     SubtractAmmo();
            // }

            //Allow the player to jump even if they have just fallen off an edge ("fall forgiveness")
            if (!grounded)
            {
                if (fallForgivenessCounter < fallForgiveness && !jumping)
                {
                    fallForgivenessCounter += Time.deltaTime;
                }
                else
                {
                    playerAnimationBehaviour.SetGroundedAnimationValue(false);
                }
            }
            else
            {
                fallForgivenessCounter = 0;
                playerAnimationBehaviour.SetGroundedAnimationValue(true);
            }

            //Set each animator float, bool, and trigger to it knows which animation to fire
            var velocityXValue = Mathf.Abs(velocity.x) / maxSpeed;
            
            playerAnimationBehaviour.UpdateMovementAnimation(velocityXValue, velocity.y, HorizontalDirection);
            playerAnimationBehaviour.UpdateAttackDirection(VerticalDirection);
            // animator.SetBool("hasChair", GameManager.Instance.inventory.ContainsKey("chair"));
            targetVelocity = move * maxSpeed;
        }
        else
        {
            //If the player is set to frozen, his launch should be zeroed out!
            launch = 0;
        }
    }

    public void SetGroundType()
    {
        //If we want to add variable ground types with different sounds, it can be done here
        switch (groundType)
        {
            case "Grass":
                stepSound = grassSound;
                break;
        }
    }

    public void Freeze(bool freeze)
    {
        //Set all animator params to ensure the player stops running, jumping, etc and simply stands
        if (freeze)
        {
            playerAnimationBehaviour.UpdateMovementAnimation(0.0f, 0.0f, 0);
            playerAnimationBehaviour.SetGroundedAnimationValue(true);
            GetComponent<PhysicsObject>().targetVelocity = Vector2.zero;
        }

        frozen = freeze;
        shooting = false;
        launch = 0;
    }


    public void GetHurt(int hurtDirection, int hitPower)
    {
        //If the player is not frozen (ie talking, spawning, etc), recovering, and pounding, get hurt!
        if (!frozen && !recoveryCounter.recovering && !pounding)
        {
            HurtEffect();
            cameraEffects.Shake(100, 1);
            playerAnimationBehaviour.SetHurtAnimationValue();
            velocity.y = hurtLaunchPower.y;
            launch = hurtDirection * (hurtLaunchPower.x);
            recoveryCounter.counter = 0;

            if (health <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                health -= hitPower;
            }

            GameManager.Instance.hud.HealthBarHurt();
        }
    }

    private void HurtEffect()
    {
        GameManager.Instance.audioSource.PlayOneShot(hurtSound);
        StartCoroutine(FreezeFrameEffect());
        GameManager.Instance.audioSource.PlayOneShot(hurtSounds[whichHurtSound]);

        if (whichHurtSound >= hurtSounds.Length - 1)
        {
            whichHurtSound = 0;
        }
        else
        {
            whichHurtSound++;
        }
        cameraEffects.Shake(100, 1f);
    }

    public IEnumerator FreezeFrameEffect(float length = .007f)
    {
        Time.timeScale = .1f;
        yield return new WaitForSeconds(length);
        Time.timeScale = 1f;
    }


    public IEnumerator Die()
    {
        if (!frozen)
        {
            dead = true;
            deathParticles.Emit(10);
            GameManager.Instance.audioSource.PlayOneShot(deathSound);
            Hide(true);
            Time.timeScale = .6f;
            yield return new WaitForSeconds(5f);
            GameManager.Instance.hud.animator.SetTrigger("coverScreen");
            GameManager.Instance.hud.loadSceneName = SceneManager.GetActiveScene().name;
            Time.timeScale = 1f;
        }
    }

    public void ResetLevel()
    {
        Freeze(true);
        dead = false;
        health = maxHealth;
    }

    public void SubtractAmmo()
    {
        if (ammo > 0)
        {
            ammo -= 20 * Time.deltaTime;
        }
    }

    public void Jump(float jumpMultiplier)
    {
        if (velocity.y != jumpPower)
        {
            velocity.y = jumpPower * jumpMultiplier; //The jumpMultiplier allows us to use the Jump function to also launch the player from bounce platforms
            PlayJumpSound();
            PlayStepSound();
            JumpEffect();
            jumping = true;
        }
    }

    public void PlayStepSound()
    {
        //Play a step sound at a random pitch between two floats, while also increasing the volume based on the Horizontal axis
        audioSource.pitch = (Random.Range(0.9f, 1.1f));
        audioSource.PlayOneShot(stepSound, Mathf.Abs(_rawInputX / 10));
    }

    public void PlayJumpSound()
    {
        audioSource.pitch = (Random.Range(1f, 1f));
        GameManager.Instance.audioSource.PlayOneShot(jumpSound, .1f);
    }


    public void JumpEffect()
    {
        jumpParticles.Emit(1);
        audioSource.pitch = (Random.Range(0.6f, 1f));
        audioSource.PlayOneShot(landSound);
    }

    public void LandEffect()
    {
        if (jumping)
        {
            jumpParticles.Emit(1);
            audioSource.pitch = (Random.Range(0.6f, 1f));
            audioSource.PlayOneShot(landSound);
            jumping = false;
        }
    }

    public void PunchEffect()
    {
        GameManager.Instance.audioSource.PlayOneShot(punchSound);
        // cameraEffects.Shake(100, 1f);
    }

    public void ActivatePound()
    {
        //A series of events needs to occur when the player activates the pound ability
        if (!pounding)
        {
            playerAnimationBehaviour.SetPoundedAnimationValue(false);

            if (velocity.y <= 0)
            {
                velocity = new Vector3(velocity.x, hurtLaunchPower.y / 2, 0.0f);
            }

            GameManager.Instance.audioSource.PlayOneShot(poundActivationSounds[Random.Range(0, poundActivationSounds.Length)]);
            pounding = true;
            FreezeFrameEffect(.3f);
        }
    }
    public void PoundEffect()
    {
        //As long as the player as activated the pound in ActivatePound, the following will occur when hitting the ground.
        if (pounding)
        {
            playerAnimationBehaviour.ResetAttackAnimation();
            velocity.y = jumpPower / 1.4f;
            playerAnimationBehaviour.SetPoundedAnimationValue(true);
            GameManager.Instance.audioSource.PlayOneShot(poundSound);
            cameraEffects.Shake(200, 1f);
            pounding = false;
            recoveryCounter.counter = 0;
            playerAnimationBehaviour.SetPoundedAnimationValue(true);
        }
    }

    public void FlashEffect()
    {
        //Flash the player quickly
        playerAnimationBehaviour.SetFlashAnimationValue();
    }

    public void Hide(bool hide)
    {
        Freeze(hide);
        foreach (SpriteRenderer sprite in graphicSprites)
            sprite.gameObject.SetActive(!hide);
    }

    public void Shoot(bool equip)
    {
        //Flamethrower ability
        if (GameManager.Instance.inventory.ContainsKey("flamethrower"))
        {
            if (equip)
            {
                if (!shooting)
                {
                    playerAnimationBehaviour.SetShootingAnimation(true);
                    GameManager.Instance.audioSource.PlayOneShot(equipSound);
                    flameParticlesAudioSource.Play();
                    shooting = true;
                }
            }
            else
            {
                if (shooting)
                {
                    playerAnimationBehaviour.SetShootingAnimation(false);
                    flameParticlesAudioSource.Stop();
                    GameManager.Instance.audioSource.PlayOneShot(holsterSound);
                    shooting = false;
                }
            }
        }
    }

    public void ReceiveMachete()
    {
        macheteData.HasReceived = true;
        machete.SetActive(true);
    }

    public void ReceiveEnergyDrink()
    {
        energyDrinkData.HasReceived = true;
        Debug.Log("Received Energy Drink ToDO: Slam Machete Into Ground and Long Jump");
        // ToDO: Slam Machete Into Ground and Long Jump
    }

    public void SetUpCheatItems()
    {
        //Allows us to get various items immediately after hitting play, allowing for testing. 
        for (int i = 0; i < cheatItems.Length; i++)
        {
            GameManager.Instance.GetInventoryItem(cheatItems[i], null);
        }
    }
    
    //Switching Action Maps ----

    public void EnableGameplayControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapPlayerControls);  
    }

    public void EnablePauseMenuControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapMenuControls);
    }
    
    //Get Data ----
    public InputActionAsset GetActionAsset()
    {
        return playerInput.actions;
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }
    
}