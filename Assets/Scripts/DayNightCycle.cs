using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{

    public static DayNightCycle instance;
    //falloff intensity is readonly 

    public Light2D sun;
    public Light2D cookoingPotLight;
    private static List<Light2D> lightSources = new List<Light2D>();


    private float cycleTime;
    private float startTime = 0;

    private float dayTime;
    private float nightTime;


    private bool day;

    private void Start()
    {
        instance = this;
        startTime = Time.time;
        cycleTime = 0.3f;
    }

    private void Update() {

        if (Time.time > startTime + cycleTime)
        {
            startTime = Time.time;

            if (sun.intensity <= 0.4)
            {
                sun.intensity = 0.41f;
                nightTime = Time.time;
                day = false;
            }
            else if (sun.intensity >= 0.6)
            {
                sun.intensity = 0.599f;
                dayTime = Time.time;
                day = true;
            }


            if (day)
            {
                if (Time.time > dayTime + 5.0f) { 
                    sun.intensity -= 0.01f;
                    cookoingPotLight.intensity += 0.01f;
                    foreach (Light2D l in lightSources)
                    {
                        l.intensity += 0.01f;
                    }
                }
            }



            else if (day == false)
            {
                if (Time.time > nightTime + 5.0f)
                {
                    sun.intensity += 0.01f;
                    cookoingPotLight.intensity -= 0.01f;
                    foreach (Light2D l in lightSources)
                    {
                        l.intensity -= 0.01f;
                    }

                }
            }

        }

    }

    public void AddLightSource(GameObject light)
    {
        Light2D l = light.transform.GetChild(0).GetComponent<Light2D>();
        lightSources.Add(l);
    }



} 
