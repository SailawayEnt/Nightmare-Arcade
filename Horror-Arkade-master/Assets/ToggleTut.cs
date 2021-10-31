using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTut : MonoBehaviour
{
    [SerializeField] GameObject tutorialGO;
    
    public void ShouldToggle(bool tutorialOn)
    {
        tutorialGO.SetActive(tutorialOn);
    } 
}
