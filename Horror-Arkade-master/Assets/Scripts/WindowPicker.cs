using UnityEngine;

[ExecuteAlways]
public class WindowPicker : MonoBehaviour
{

    enum WindowStyle
    {
        Style1,
        Style2,
        Style3
    }; 

    [SerializeField] WindowStyle windowStyle;

    [SerializeField] GameObject[] windows = new GameObject[3];

    void Awake()
    {
      ChangeWindows();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            ChangeWindows();
        }
    }

    void ChangeWindows()
    {
        if (windowStyle == WindowStyle.Style1)
        {
            windows[0].SetActive(true);
            windows[1].SetActive(false);
            windows[2].SetActive(false);
        }else if (windowStyle == WindowStyle.Style2)
        {
            windows[0].SetActive(false);
            windows[1].SetActive(true);
            windows[2].SetActive(false);
        }
        else if(windowStyle == WindowStyle.Style3)
        {
            windows[0].SetActive(false);
            windows[1].SetActive(false);
            windows[2].SetActive(true);
        }
    }
}