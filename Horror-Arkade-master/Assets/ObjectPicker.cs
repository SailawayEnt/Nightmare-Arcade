using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ObjectPicker : MonoBehaviour, ISerializationCallbackReceiver
{
    public static List<string> TMPList;
    [HideInInspector]public List<string> PopupList;

    [ListToPopup(typeof(ObjectPicker), "TMPList")] 
    public string Styles;

    [SerializeField]  List<GameObject> objects;

    int _currentIndex = 0;

    GameObject _currentGO;

    public List<string> ObjectList()
    {
        List<string> objectNames = new List<string>();

        foreach (var go in objects)
        {
            objectNames.Add(go.name);
        }

        return objectNames;
    }
    
    void Awake()
    {
        ChangeObject();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            ChangeObject();
        }
    }

    void ChangeObject()
    {
        foreach (var go in objects)
        {
            if (TMPList[_currentIndex] != null)
            {
                go.SetActive(TMPList[_currentIndex] == go.name);
            }
                
        }
    }
    
    public void OnBeforeSerialize()
    {
        _currentIndex = ListToPopupDrawer.ReturnIndex();
        PopupList = ObjectList();
        TMPList = PopupList;
    }

    public void OnAfterDeserialize(){}
}
