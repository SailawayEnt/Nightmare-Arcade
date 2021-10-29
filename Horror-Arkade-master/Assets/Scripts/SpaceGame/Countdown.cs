using System.Collections;
using TMPro;
using UnityEngine;

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
            if (_countdown == 10)
            {
                countdownNumbers.alignment = TextAlignmentOptions.Center;
                countdownNumbers.text = $"{_countdown.ToString()}";
            }
            else
            {
                countdownNumbers.alignment = TextAlignmentOptions.Left;
                countdownNumbers.text = $"0{_countdown.ToString()}";
            }


            yield return new WaitForSeconds(1f);

            _countdown--;
        }

        countdownNumbers.text = "00";
        
         //todo: exit game
    }
}
