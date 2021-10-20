using System;
using UnityEngine;

/// <summary>
/// This script defines the borders of ‘Player’s’ movement. Depending on the chosen handling type, it moves the ‘Player’ together with the pointer.
/// </summary>

[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour {

    [Tooltip("offset from viewport borders for player's movement")]
    public Borders borders;
    Camera mainCamera;
    bool controlIsActive = true;
    [SerializeField][Min(1)]float speedMultiplier = 5.0f;

    [SerializeField] Animator handsAnim;

    public static PlayerMoving instance; //unique instance of the script for easy access to the script

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders();                //setting 'Player's' moving borders depending on Viewport's size
    }

    private void Update()
    {
        if (controlIsActive)
        {
#if UNITY_STANDALONE || UNITY_EDITOR    //if the current platform is not mobile, setting mouse handling 

            if (Player.instance.InputMovement.x != 0) //if mouse button was pressed       
            {
                handsAnim.SetInteger("moveDirection", Player.instance.HorizontalDirection);
                Vector3 movement = Player.instance.InputMovement * speedMultiplier;
                transform.position += movement * Time.deltaTime;
            }
#endif

#if UNITY_IOS || UNITY_ANDROID //if current platform is mobile, 

            Debug.Log("mobile version?")
#endif
            var position = transform.position;
            position = new Vector3    //if 'Player' crossed the movement borders, returning him back 
                (
                Mathf.Clamp(position.x, borders.minX, borders.maxX),
                Mathf.Clamp(position.y, borders.minY, borders.maxY),
                0
                );
            transform.position = position;
        }
    }

    //setting 'Player's' movement borders according to Viewport size and defined offset
    void ResizeBorders() 
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }
}
