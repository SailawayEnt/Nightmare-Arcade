using UnityEngine;

public class DestroyMyself : MonoBehaviour
{
    public void Destroy()
    {
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }
}
