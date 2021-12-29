using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemMenu : MonoBehaviour
{
    private GameObject totem;
    public List<GameObject> recipes;
    public List<GameObject> r1; //recipe 1 items required
    public List<GameObject> r2;

    void OnEnable()
    {
        if (TotemManager.instance.GetCurrentTotemLevel() == 1) { 
            foreach (GameObject item in r1)
            {
                Text t = item.transform.GetChild(0).gameObject.GetComponent<Text>();

                int amountOfItem = PlayerResources.instance.GetResourceCount(item.GetComponent<TotemItem>().index);
                int necessaryAmount = item.GetComponent<TotemItem>().necessaryAmount;
                if (amountOfItem >= necessaryAmount) amountOfItem = necessaryAmount;
                t.text = amountOfItem.ToString() + " / " + necessaryAmount.ToString();
            }
        }




    }

    //called on button press
    public void UpgradeTotem()
    {
        if (TotemManager.instance.GetCurrentTotemLevel() == 1)
        {
            if (Upgrade1()) { 
                TotemManager.instance.UpgradeCurrentTotemLevel();
                foreach (GameObject recipe in recipes)
                {
                    recipe.SetActive(false);
                }
                //recipes[1].SetActive(true);
                totem.GetComponent<Totem>().UpgradeTotemSprite();
            }
        }

        //2nd last
        //if (TotemManager.instance.GetCurrentTotemLevel() == 3)
        //{
        //    if (Upgrade3())
        //    {
        //        //add some sort of warning that this will lock doors and the boss will spawn
        //        //lock doors
        //        //spawn boss
        //        //disable totem
        //        //after boss dies instantitate travelling beacon
        //    }
        //}

    }

    public bool Upgrade1()
    {
        if (PlayerResources.instance.GetWoodCount() >= 10)
        {
            if (PlayerResources.instance.GetRockCount() >= 10)
            {
                Buy1();
                return true;
            }
        }
        return false;
    }
    public void Buy1()
    {
        PlayerResources.instance.AddResource(-10, 0);
        PlayerResources.instance.AddResourceToInventory(-10, 0);
        PlayerResources.instance.AddResource(-10, 1);
        PlayerResources.instance.AddResourceToInventory(-10, 1);
    }



    public void GetTotem(GameObject t)
    {
        totem = t;
    }

}
