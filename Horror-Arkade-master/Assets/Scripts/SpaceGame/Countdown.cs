using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownNumbers;
    int _countdown;

    [SerializeField] GameCabinetMenu gameManager;

    void OnEnable()
    {
        _countdown = 10;
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
        yield return new WaitForSeconds(.5f);
        gameManager.HideContinueMenu();
        gameManager.ExitCabinet();
    }
}
