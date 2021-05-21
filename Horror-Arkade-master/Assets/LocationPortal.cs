using UnityEngine;
using Cinemachine;

public class LocationPortal : MonoBehaviour
{
    
    [Header("Target")]
    [SerializeField] Transform targetDestination;

    [SerializeField] private CinemachineVirtualCamera currentVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera destinationVirtualCamera;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var position = targetDestination.position;
        
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            
            currentVirtualCamera.Priority = 1; 
            destinationVirtualCamera.Priority = 2;
            
            col.transform.position = new Vector2(position.x, position.y);
        }
    }
}
