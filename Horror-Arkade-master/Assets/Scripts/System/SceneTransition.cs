using System;
using Cinemachine;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [Header ("Reference")]
    [SerializeField] ScenesData scenesData;
    [SerializeField] int sceneToLoad;
    [SerializeField] CinemachineVirtualCamera currentVirtualCamera;
    [SerializeField] CinemachineVirtualCamera destinationVirtualCamera;
    [SerializeField] Vector2 playerPosition;
    [SerializeField] Vector2Value playerPositionStorage;
    [SerializeField] GameEvent onSceneChange;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject && !col.isTrigger)
        {
            playerPositionStorage.initialValue = playerPosition;
            scenesData.LoadLevelWithIndex(sceneToLoad);
            // onSceneChange?.Invoke();
            // currentVirtualCamera.Priority = 1; 
            // destinationVirtualCamera.Priority = 2;
        }
    }
}
