using UnityEngine;

[ExecuteAlways]
public class BridgePicker : MonoBehaviour
{

    enum BridgeStyle
    {
        Style1,
        Style2,
    }; 

    [SerializeField] BridgeStyle boxStyle;

    [SerializeField] GameObject[] bridges = new GameObject[2];

    void Awake()
    {
        ChangeBoxes();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            ChangeBoxes();
        }
    }

    void ChangeBoxes()
    {
        switch (boxStyle)
        {
            case BridgeStyle.Style1:
                bridges[0].SetActive(true);
                bridges[1].SetActive(false);
                break;
            case BridgeStyle.Style2:
                bridges[0].SetActive(false);
                bridges[1].SetActive(true);
                break;
        }
    }
}