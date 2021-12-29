using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGeneratorEnergy : MonoBehaviour
{

    public static PowerGeneratorEnergy instance;
    public Image mask;
    public Image mask2;
    private bool energized;
    private bool timer;
    private float energyLevel = 1f;
    float nextDecreaseTime = 0f;
    public float incrementRate = 1.0f;


    private void Start()
    {
        instance = this;
        timer = true;
        energized = true;
    }

    private void Update()
    {
        if (energized)
        {
            if (Time.time >= nextDecreaseTime) {


                energyLevel -= 0.05f;
                mask.fillAmount = energyLevel;
                mask2.fillAmount = energyLevel;

                if (energyLevel <= 0)
                {
                    energized = false;
                }

                nextDecreaseTime = Time.time + 1f / incrementRate;

            }
        }
    }


    public bool GetEnergized()
    {
        return energized;
    }

    public void SetEnergized(bool energy)
    {
        energized = energy;
    }

    public void SetEnergyLevel()
    {
        //check for number of crystals
        
        energyLevel = 1f;
    }


}
