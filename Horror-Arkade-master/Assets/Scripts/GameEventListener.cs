using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent; 
    [SerializeField] UnityEvent response; 

    void Awake() => gameEvent.Register(this);

    void OnDestroy() => gameEvent.Unregister(this);

    public void RaiseEvent() => response.Invoke();
}