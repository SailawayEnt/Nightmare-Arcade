﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ListToPopupAttribute : PropertyAttribute
{
    public System.Type MyType;
    public string PropertyName;
    public ListToPopupAttribute(System.Type myType, string propertyName)
    {
        MyType = myType;
        PropertyName = propertyName;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ListToPopupAttribute))]
public class ListToPopupDrawer : PropertyDrawer
{
    public static int index;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ListToPopupAttribute atb = attribute as ListToPopupAttribute;
        List<string> stringList = null;

        if (atb.MyType.GetField(atb.PropertyName) != null)
        {
            stringList = atb.MyType.GetField(atb.PropertyName).GetValue(atb.MyType) as List<string>;
        }

        if (stringList != null && stringList.Count != 0)
        {
            int selectedIndex = Mathf.Max(stringList.IndexOf(property.stringValue), 0);
            selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, stringList.ToArray());
            property.stringValue = stringList[selectedIndex];
            index = selectedIndex;
            ReturnIndex();
        }
        else EditorGUI.PropertyField(position, property, label);
    }
    
    public static int ReturnIndex()
    {
        return index;
    }
}
#endif

