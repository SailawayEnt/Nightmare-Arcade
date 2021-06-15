using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenePartLoader : MonoBehaviour
{
    //Scene state
    bool _isLoaded;
    bool _shouldLoad;
    
    
    void Start()
    {
        //verify if the scene is already open to avoid opening a scene twice
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == gameObject.name)
                {
                    _isLoaded = true;
                }
            }
        }
    }

    void Update()
    {
        TriggerCheck();
    }
    

    void LoadScene()
    {
        if (!_isLoaded)
        {
            StartCoroutine(LoadAsync());
        }
    }

    IEnumerator LoadAsync()
    {
        //Loading the scene, using the gameobject name as it's the same as the name of the scene to load
        AsyncOperation aoLevel =SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
        //We set it to true to avoid loading the scene twice
        _isLoaded = true;
            
        while (!aoLevel.isDone)
            yield return null;
        
    }

    void UnLoadScene()
    {
        if (_isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            _isLoaded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (NewPlayer.Instance)
        {
            if (col.gameObject == NewPlayer.Instance.gameObject)
            {
                _shouldLoad = true;
            }
        }else if (CabinetPlayer.Instance)
        {
            if (col.gameObject == CabinetPlayer.Instance.gameObject)
            {
                _shouldLoad = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (NewPlayer.Instance)
        {
            if (col.gameObject == NewPlayer.Instance.gameObject)
            {
                _shouldLoad = false;
            }
        }else if (CabinetPlayer.Instance)
        {
            if (col.gameObject == CabinetPlayer.Instance.gameObject)
            {
                _shouldLoad = false;
            }
        }
    }

    void TriggerCheck()
    {
        //shouldLoad is set from the Trigger methods
        if (_shouldLoad)
        {
            LoadScene();
        }
        else
        {
            UnLoadScene();
        }
    }

}

