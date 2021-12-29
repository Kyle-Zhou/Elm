using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    public static EnergyBar instance;
    public Image mask;
    public GameObject player;

    private float energy;
    private float energyTiredBound = 0.1f;
    private float previousDash;
    private bool full, regenerating;

    private void Start()
    {
        instance = this;
        energy = 1;
        full = true;
    }

    private void Update()
    {
        if (energy < 1.0f)
        {
            full = false;
        }
        if (Time.time - previousDash > 1.5f && !regenerating)
        {
            StartCoroutine("Regeneration");
        } 
        else
        {
            StopCoroutine("Regeneration");
            regenerating = false;
        }
        mask.fillAmount = energy;
    }

    public void GainEnergy(float gainedAmount)
    {
        energy += gainedAmount;
        if (energy >= 1.0f)
        {
            full = true;
        }
        if (energy > 1.0f)
        {
            energy = 1.0f;
        }
    }

    public float GetEnergy()
    {
        return energy;
    }

    public void SetEnergy(float energyAmount)
    {
        energy = energyAmount;
    }

    public bool GetFull()
    {
        return full;
    }

    public float GetEnergyTiredBound()
    {
        return energyTiredBound;
    }

    public void SetPreviousDash(float previousDash)
    {
        this.previousDash = previousDash;
    }
    
    private IEnumerator Regeneration()
    {
        regenerating = true;
        if (!full)
        {
            GainEnergy(0.01f);
        }
        yield return new WaitForSeconds(2f);
    }
}
