using System;
using UnityEngine;

public class WhenDestroyed : MonoBehaviour
{
    [SerializeField] GameEvent onTicketReceived;
    [SerializeField] GameEvent onGameWon;
    [SerializeField] ConsumableItem ticketInventory;
    [SerializeField] ScenesData scenesData;
    GameObject _player;
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnDestroy()
    {
        if (!_player.activeSelf) return;

        if (ticketInventory.CurrentStack < ticketInventory.MaxStack)
        {
            Debug.Log("game won and ticket given");
            ticketInventory.CurrentStack += 1;
            onTicketReceived?.Invoke();
        }
        scenesData.LoadLevelWithIndex(2);
        onGameWon?.Invoke();
    }
}
