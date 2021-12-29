using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatingScript : MonoBehaviour
{
    //public GameObject energyBar;
    public static EatingScript instance;

    public void Start()
    {
        instance = this;
    }


    //turn this into array or something

    public void EatBlueberry()
    {
        if (PlayerResources.instance.GetBlueberryCount() >= 1)
        {
            if (EnergyBar.instance.GetEnergy() + 0.011f > 1)
            {
                EnergyBar.instance.GainEnergy(1 - EnergyBar.instance.GetEnergy());
                PlayerResources.instance.AddResource(-1, 11);
                PlayerResources.instance.AddResourceToInventory(-1, 11);

            } else
            {
                EnergyBar.instance.GainEnergy(0.2f);
                PlayerResources.instance.AddResource(-1, 11);
                PlayerResources.instance.AddResourceToInventory(-1, 11);

            }

            if (HealthBar.instance.GetHealth() >= 100)
            {

            } else
            {
                HealthBar.instance.AddHealth(10);
                //PlayerResources.instance.AddResource(-1, 11);
                //PlayerResources.instance.AddResourceToInventory(-1, 11);

            }
        }

       
    }

    public void EatCarrot()
    {
        if (PlayerResources.instance.GetCarrotCount() >= 1)
        {
            if (EnergyBar.instance.GetEnergy() + 0.01f > 1)
            {
                EnergyBar.instance.GainEnergy(1 - EnergyBar.instance.GetEnergy());
                PlayerResources.instance.AddResource(-1, 12);
                PlayerResources.instance.AddResourceToInventory(-1, 12);

            }
            else
            {
                EnergyBar.instance.GainEnergy(0.5f);
                PlayerResources.instance.AddResource(-1, 12);
                PlayerResources.instance.AddResourceToInventory(-1, 12);

            }

            if (HealthBar.instance.GetHealth() >= 100)
            {

            }
            else
            {
                HealthBar.instance.AddHealth(10);
                //PlayerResources.instance.AddResource(-1, 12);
                //PlayerResources.instance.AddResourceToInventory(-1, 12);

            }
        }
    }

    public void EatBread()
    {
        if (PlayerResources.instance.GetBreadCount() >= 1)
        {
            if (EnergyBar.instance.GetEnergy() + 0.01f > 1)
            {
                EnergyBar.instance.GainEnergy(1 - EnergyBar.instance.GetEnergy());
                PlayerResources.instance.AddResource(-1, PlayerResources.instance.GetBreadIndex());
                PlayerResources.instance.AddResourceToInventory(-1, PlayerResources.instance.GetBreadIndex());

            }
            else
            {
                EnergyBar.instance.GainEnergy(0.5f);
                PlayerResources.instance.AddResource(-1, PlayerResources.instance.GetBreadIndex());
                PlayerResources.instance.AddResourceToInventory(-1, PlayerResources.instance.GetBreadIndex());

            }

            if (HealthBar.instance.GetHealth() >= 100)
            {

            }
            else
            {
                HealthBar.instance.AddHealth(20);
                //PlayerResources.instance.AddResource(-1, 12);
                //PlayerResources.instance.AddResourceToInventory(-1, 12);

            }
        }
    }

    public void EatPie()
    {
        if (PlayerResources.instance.GetCarrotCount() >= 1)
        {
            if (EnergyBar.instance.GetEnergy() + 0.01f > 1)
            {
                EnergyBar.instance.GainEnergy(1 - EnergyBar.instance.GetEnergy());
                PlayerResources.instance.AddResource(-1, PlayerResources.instance.GetBlueberryPieIndex());
                PlayerResources.instance.AddResourceToInventory(-1, PlayerResources.instance.GetBlueberryPieIndex());

            }
            else
            {
                EnergyBar.instance.GainEnergy(0.5f);
                PlayerResources.instance.AddResource(-1, PlayerResources.instance.GetBlueberryPieIndex());
                PlayerResources.instance.AddResourceToInventory(-1, PlayerResources.instance.GetBlueberryPieIndex());

            }

            if (HealthBar.instance.GetHealth() >= 100)
            {

            }
            else
            {
                HealthBar.instance.AddHealth(20);
                //PlayerResources.instance.AddResource(-1, 12);
                //PlayerResources.instance.AddResourceToInventory(-1, 12);

            }
        }
    }


}


