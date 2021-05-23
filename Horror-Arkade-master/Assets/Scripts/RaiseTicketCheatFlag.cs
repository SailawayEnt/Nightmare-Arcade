using UnityEngine;

public class RaiseTicketCheatFlag : MonoBehaviour
{
  [SerializeField] private GameEvent onCollectedTicket;

  private void Awake()
  {
    onCollectedTicket.Invoke();
  }
}
