using UnityEngine;


public class TriggerDoorController : MonoBehaviour
{
    /*[SerializeField] Animator myDoor = null;

    [SerializeField] bool openTrigger = false;
    [SerializeField] bool closeTrigger = false;*/

    Animator _animator;

    bool _doorOpen;

    void Start()
    {
        _doorOpen = false;
        _animator = GetComponent<Animator>();
    }
    /*
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            if (openTrigger)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);
                
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        
        
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            if (closeTrigger)
            {
                myDoor.Play("DoorClose", 0, 0.0f);
            }
        }
    }*/
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject && !_doorOpen)
        {
            _doorOpen = true;
            DoorControl("Open");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (_doorOpen)
        {
            _doorOpen = false;
            DoorControl("Close");
        }
    }

    void DoorControl(string direction)
    {
        _animator.SetTrigger((direction));
    }
}
