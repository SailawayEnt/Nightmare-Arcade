using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class SpaceTutorial : MonoBehaviour
{
   const float TIME_TO_WAIT = 10f;
   
   [SerializeField] VideoPlayer backgroundVideo;
   [SerializeField] FadeInOutUI mainLogo;
   [SerializeField] FadeInOutUI tutorial;
   [SerializeField] FadeInOutUI movement;
   [SerializeField] FadeInOutUI waves;
   [SerializeField] FadeInOutUI dodge;
   [SerializeField] FadeInOutUI collect;
   [SerializeField] FadeInOutUI destroy;
   void Awake()
   {
      StartCoroutine(StartTut());
   }

   IEnumerator StartTut()
   {
      while (true)
      {
         ShowMainLogo();  
         yield return new WaitForSeconds(TIME_TO_WAIT);
         ShowTutorial(movement);
         yield return new WaitForSeconds(TIME_TO_WAIT);
         HideTutorial(movement);
         yield return new WaitForSeconds(TIME_TO_WAIT);
         ShowTutorial(waves);
         yield return new WaitForSeconds(TIME_TO_WAIT);
         HideTutorial(waves);
         yield return new WaitForSeconds(TIME_TO_WAIT);
         ShowTutorial(dodge);
         yield return new WaitForSeconds(TIME_TO_WAIT);
         HideTutorial(dodge);
         yield return new WaitForSeconds(TIME_TO_WAIT);
         ShowTutorial(collect);
         yield return new WaitForSeconds(TIME_TO_WAIT);
         HideTutorial(collect);
         yield return new WaitForSeconds(TIME_TO_WAIT);
         ShowTutorial(destroy);
         yield return new WaitForSeconds(TIME_TO_WAIT);
         HideTutorial(destroy);
      }
   }
   void ShowMainLogo()
   {
      mainLogo.ShowUI(true);
   }
   void ShowTutorial(FadeInOutUI tutState)
   {
      mainLogo.ShowUI(false);
      if (backgroundVideo.isPlaying)
      {
        // backgroundVideo.Pause();
      }
      tutorial.ShowUI(true);
      tutState.ShowUI(true);
   }

   void HideTutorial(FadeInOutUI tutState)
   {
      if (backgroundVideo.isPaused)
      {
         backgroundVideo.Play();   
      }
      ShowMainLogo();
      tutorial.ShowUI(false);
      tutState.ShowUI(false);
   }
   
}