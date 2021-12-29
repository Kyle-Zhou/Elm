using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueberryBushStates : HarvestableStates
{
    BlueberryBushStates()
    {
        health = 1;
    }
    public override void Collect()
    {
        if (mature)
        {
            SoundManager.PlaySound(SoundManager.Sound.CollectBlueberries);
            mature = false;
            sr.sprite = leftover;
            //PlayerResources.instance.AddBlueberries(3);
            PlayerResources.instance.AddResource(1, 11);
            PlayerResources.instance.AddResourceToInventory(1, 11);
            timeOfHarvest = Time.time;
        }
    }

    public override void MachineCollect()
    {
        health--;

        if (health <= 0)
        {
            if (mature)
            {
                mature = false;
                sr.sprite = leftover;
                timeOfHarvest = Time.time;
                health = 1;
            }
        }
    }
}
