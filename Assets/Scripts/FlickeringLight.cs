using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private float intensityModifier = 0.1f;
    [SerializeField] private float flickerFrequency = 0.1f;

    private Light thisLight;
    private float initialIntensity;

    private void Start()
    {
        thisLight = GetComponent<Light>();
        initialIntensity = thisLight.intensity;
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            thisLight.intensity = Random.Range(initialIntensity - intensityModifier, initialIntensity + intensityModifier);

            yield return new WaitForSeconds(flickerFrequency);
        }
    }
}
