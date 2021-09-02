using UnityEngine;

[RequireComponent(typeof(RecoveryCounter))]
public class radio : MonoBehaviour
{
    [SerializeField] bool radioOn;

    [SerializeField] GameObject onScreen;
    [SerializeField] GameObject offScreen;
    
    [SerializeField]  RecoveryCounter recoveryCounter;
    [SerializeField] Animator iconAnimator; //The E icon animator
    private static readonly int Active = Animator.StringToHash("active");
    [SerializeField] private bool sleeping;

    void Start()
    {
        recoveryCounter = GetComponent<RecoveryCounter>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject && NewPlayer.Instance.grounded && !recoveryCounter.recovering)
        {
            iconAnimator.SetBool(Active, true);
            if (NewPlayer.Instance.InteractPressedValue > 0)
            {
                //Ensure the player can't hit the box multiple times in one hit
                recoveryCounter.counter = 0;
                radioOn = !radioOn;
                if (radioOn)
                {
                    onScreen.SetActive(true);
                    offScreen.SetActive(false);
                }else
                {
                    offScreen.SetActive(true);
                    onScreen.SetActive(false);
                }
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            iconAnimator.SetBool(Active, false);
        }
    }
    
}


