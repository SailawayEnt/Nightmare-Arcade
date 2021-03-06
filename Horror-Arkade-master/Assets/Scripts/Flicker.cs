using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;


[DisallowMultipleComponent]
public class Flicker : MonoBehaviour
{
    Light2D _theLight;
    
    [SerializeField] bool disableFlicker;
    
    [SerializeField]
    float minFlickerTime;
    [SerializeField]
    float maxFlickerTime;

    [SerializeField]
    bool randomIntensity;
    [SerializeField]
    float minIntensity;
    [SerializeField]
    float maxIntensity;

    void Awake()
    {
        _theLight = GetComponentInChildren<Light2D>();
    }

    public void Start()
    {
        StartCoroutine(Flickering());
    }

    IEnumerator Flickering()
    {
        while (!disableFlicker)
        {
            float randomIntensityValue = Random.Range(minIntensity, maxIntensity);
            float randomTime = Random.Range(minFlickerTime, maxFlickerTime);

            if (randomIntensity)
                _theLight.intensity = randomIntensityValue;
            yield return new WaitForSeconds(randomTime);
            _theLight.enabled = !_theLight.enabled;
        }
    }
}