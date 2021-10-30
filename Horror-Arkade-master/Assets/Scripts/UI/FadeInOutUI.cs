using UnityEngine;

public class FadeInOutUI : MonoBehaviour
{
    [SerializeField] CanvasGroup myUIGroup;   
    bool _fadeIn;
    bool _fadeOut;

    public void ShowUI(bool showUI)
    {
        if (showUI)
        {
            _fadeIn = true;
        }
        else
        {
            _fadeOut = true;
        }
    }

    void Update()
    {
        if (_fadeIn)
        {
            if (myUIGroup.alpha < 1)
            {
                myUIGroup.alpha += Time.deltaTime;
                if (myUIGroup.alpha >= 1)
                {
                    _fadeIn = false;
                }
            }
        }

        if (!_fadeOut) return;
        if (!(myUIGroup.alpha >= 0)) return;
        myUIGroup.alpha -= Time.deltaTime;
        if (myUIGroup.alpha == 0)
        {
            _fadeOut = false;
        }
    }
}
