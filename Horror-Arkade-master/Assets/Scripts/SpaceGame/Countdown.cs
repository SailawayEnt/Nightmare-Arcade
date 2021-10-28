using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class Countdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownNumbers;
    int _countdown = 10;

    void OnEnable()
    {
        StartCoroutine(CountdownToExit());
    }

    IEnumerator CountdownToExit()
    {
        while (_countdown > 0)
        {
            countdownNumbers.text = _countdown == 10 ? _countdown.ToString() : $"0{_countdown.ToString()}";

            yield return new WaitForSeconds(1f);

            _countdown--;
        }

        countdownNumbers.text = "00";
        
         //todo: exit game
    }
}
