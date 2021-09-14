using UnityEngine;

[ExecuteAlways]
public class Grunge : MonoBehaviour
{
    [SerializeField] GameObject grunge;
    [SerializeField] bool enableGrunge = true;

    void Awake()
    {
       ChangeGrunge();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            ChangeGrunge();
        }
    }

    void ChangeGrunge()
    {
        if (grunge != null)
        {
            grunge.SetActive(enableGrunge);
        }
    }
}
