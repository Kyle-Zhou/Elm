using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;

    public static bool shake;
    private float recovery = 1, frequency, reduction;

    public float rotationMultiplier = 15f;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    { 
        if (shake)
        {
            transform.position = transform.position + new Vector3(((Mathf.PerlinNoise(0, Time.time * frequency) * 2 - 1) * 0.5f * recovery) / reduction, ((Mathf.PerlinNoise(1, Time.time * frequency) * 2 - 1) * 0.5f * recovery) / reduction, 0);
            recovery = Mathf.Clamp01(recovery - 1.5f * Time.deltaTime);

            if (recovery == 0)
            {
                shake = false;
            }
        }
    }

    public void Shake(float frequency, float reduction)
    {
        recovery = 1;
        shake = true;
        this.frequency = frequency;
        this.reduction = reduction;
    }
}
