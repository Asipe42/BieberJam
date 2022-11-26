using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] Light2D light;
    [SerializeField] float minIntensity;
    [SerializeField] float maxIntensity;

    bool onIncrease;

    void Start()
    {
        StartCoroutine(ChangeIntensity());
    }

    IEnumerator ChangeIntensity()
    { 
        while (true)
        {
            if (light.intensity < minIntensity)
            {
                onIncrease = true;
            }
            else if (light.intensity > maxIntensity)
            {
                onIncrease = false;
            }

            if (onIncrease)
            {
                light.intensity += 0.01f;
            }
            else
            {
                light.intensity -= 0.01f;
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
