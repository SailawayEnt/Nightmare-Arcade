using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneControlManager : Singleton<SceneControlManager>
{
    const float TRANSPARENT = 0;
    const float OPAQUE = 1;
    
    public Image fader;
    
    [SerializeField] Vector2Value playerPositionStorage;
    void Awake()
    {
        DontDestroyChildOnLoad(gameObject);

        fader.rectTransform.sizeDelta = new Vector2(Screen.width + 20, Screen.height + 20);
        fader.gameObject.SetActive(false);
    }
    
    public static void LoadScene(int index, float duration = 1, float waitTime = 0)
    {
        Instance.StartCoroutine(Instance.FadeScene(index, duration, waitTime));
    }
    
    IEnumerator FadeScene(int index, float duration, float waitTime)
    {
        fader.gameObject.SetActive(true);

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(TRANSPARENT, OPAQUE, t));
            yield return null;
        }
        
        
        AsyncOperation aoGameplay = SceneManager.LoadSceneAsync("Gameplay" + index.ToString());
        // SceneManager.LoadSceneAsync("Level" + index.ToString() + "Part1", LoadSceneMode.Additive);

        while (!aoGameplay.isDone)
            yield return null;
        
        yield return new WaitForSeconds(waitTime);
        
        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(OPAQUE, TRANSPARENT, t));
            yield return null;
        }
        
        fader.gameObject.SetActive(false);
    }

    public static void ShowOnlyFader(float duration = 1, float waitTime = 0)
    {
        Instance.StartCoroutine(Instance.FadeSceneWithoutLoad(duration, waitTime));
    }
    
    IEnumerator FadeSceneWithoutLoad(float duration, float waitTime)
    {
        fader.gameObject.SetActive(true);

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(TRANSPARENT, OPAQUE, t));
            yield return null;
        }
        
        NewPlayer.Instance.transform.position = playerPositionStorage.initialValue;
        
        yield return new WaitForSeconds(waitTime);
        
        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(OPAQUE, TRANSPARENT, t));
            yield return null;
        }
        
        fader.gameObject.SetActive(false);
    }
}
