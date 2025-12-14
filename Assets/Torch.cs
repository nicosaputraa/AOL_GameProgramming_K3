using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WallTorchFlicker : MonoBehaviour
{
    Light2D light2D;

    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;
    public float flickerSpeed = 0.1f;

    void Awake()
    {
        light2D = GetComponent<Light2D>();
        InvokeRepeating(nameof(Flicker), 0f, flickerSpeed);
    }

    void Flicker()
    {
        light2D.intensity = Random.Range(minIntensity, maxIntensity);
    }
}
