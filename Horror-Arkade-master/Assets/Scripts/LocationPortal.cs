using UnityEngine;
using Cinemachine;

public class LocationPortal : MonoBehaviour
{
    
    [Header("Target")]
    [SerializeField] Transform targetDestination;
    [SerializeField] GameObject thePlayer;

    [SerializeField] private CinemachineVirtualCamera currentVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera destinationVirtualCamera;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var position = targetDestination.position;
        
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            thePlayer.transform.position = targetDestination.transform.position;
            
            currentVirtualCamera.Priority = 1; 
            destinationVirtualCamera.Priority = 2;
        }
    }
}
