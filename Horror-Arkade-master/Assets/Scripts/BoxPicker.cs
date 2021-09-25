using UnityEngine;

[ExecuteAlways]
public class BoxPicker : MonoBehaviour
{

    enum WindowStyle
    {
        Style1,
        Style2,
        Style3,
        Style4
    }; 

    [SerializeField] WindowStyle boxStyle;

    [SerializeField] GameObject[] boxes = new GameObject[4];

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
            case WindowStyle.Style1:
                boxes[0].SetActive(true);
                boxes[1].SetActive(false);
                boxes[2].SetActive(false);
                boxes[3].SetActive(false);
                break;
            case WindowStyle.Style2:
                boxes[0].SetActive(false);
                boxes[1].SetActive(true);
                boxes[2].SetActive(false);
                boxes[3].SetActive(false);
                break;
            case WindowStyle.Style3:
                boxes[0].SetActive(false);
                boxes[1].SetActive(false);
                boxes[2].SetActive(true);
                boxes[3].SetActive(false);
                break;
            case WindowStyle.Style4:
                boxes[0].SetActive(false);
                boxes[1].SetActive(false);
                boxes[2].SetActive(false);
                boxes[3].SetActive(true);
                break;
        }
    }
}