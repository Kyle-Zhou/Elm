using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionSystem : MonoBehaviour
{

    public Slider bar;

    public Text text;

    private int resourceAmount;
    private GameObject buildingInstance;

    //public Slider bar;

    //public Text woodText;

    //private int woodAmount;
    //private int blueberries;
    //private int goldAmount;
    //private GameObject chopperBuilding;

    private void Start()
    {
        //woodText.text = woodAmount.ToString() + "/100";
        //bar.value = woodAmount;

        text.text = resourceAmount.ToString() + "/100";
        bar.value = resourceAmount;

    }

    //public int GetGold()
    //{
    //    return goldAmount;
    //}

    //public int GetBlueberries()
    //{
    //    return blueberries;
    //}

    //public int GetWood()
    //{
    //    return woodAmount;
    //}

    public void AddResource(int added)
    {
        if (resourceAmount + added >= 100)
        {
            buildingInstance.GetComponent<AutoChopping>().enabled = false;
            resourceAmount = 100;
            text.text = resourceAmount.ToString() + "/100";
            bar.value = resourceAmount;
        }
        else
        {
            this.resourceAmount += added;
            text.text = resourceAmount.ToString() + "/100";
            bar.value = resourceAmount;
        }
    }


    public void CollectResource()
    {
        if (buildingInstance.CompareTag("WoodChopper")) { 
        buildingInstance.GetComponent<AutoChopping>().enabled = true;

            PlayerResources.instance.AddResource(resourceAmount, 0);
            PlayerResources.instance.AddResourceToInventory(resourceAmount, 0);

        } else if (buildingInstance.CompareTag("AutoPicker")) {

            buildingInstance.GetComponent<AutoPicking>().enabled = true;
            //PlayerResources.instance.AddBlueberries(resourceAmount);

        } else if (buildingInstance.CompareTag("GoldMine"))
        {
            buildingInstance.GetComponent<GoldMining>().enabled = true;
            //PlayerResources.instance.AddGold(resourceAmount);
        }


        resourceAmount -= resourceAmount;
        text.text = resourceAmount.ToString() + "/100";
        bar.value = resourceAmount;
    }

    public void SetChopperBuilding(GameObject reference)
    {
        //chopperBuilding = wsm;
        buildingInstance = reference;

    }

    //public void AddWood(int wood)
    //{
    //    if (woodAmount + wood >= 100)
    //    {
    //        chopperBuilding.GetComponent<AutoChopping>().enabled = false;
    //        woodAmount = 100;
    //        woodText.text = woodAmount.ToString() + "/100";
    //        bar.value = woodAmount;
    //    } else { 
    //        this.woodAmount += wood;
    //        woodText.text = woodAmount.ToString() + "/100";
    //        bar.value = woodAmount;
    //    }
    //}

    //public void CollectWood()
    //{
    //    chopperBuilding.GetComponent<AutoChopping>().enabled = true;
    //    PlayerResources.instance.AddWood(woodAmount);
    //    woodAmount -= woodAmount;
    //    woodText.text = woodAmount.ToString() + "/100";
    //    bar.value = woodAmount;
    //}

    //public void AddBlueberries(int blueberry)
    //{
    //    if (blueberries + blueberry >= 100)
    //    {
    //        chopperBuilding.GetComponent<AutoPicking>().enabled = false;
    //        blueberries = 100;
    //        woodText.text = blueberries.ToString() + "/100";
    //        bar.value = blueberries;
    //    }
    //    else
    //    {
    //        this.blueberries += blueberry;
    //        woodText.text = blueberries.ToString() + "/100";
    //        bar.value = blueberries;
    //    }
    //}

    //public void CollectBlueberries()
    //{
    //    chopperBuilding.GetComponent<AutoPicking>().enabled = true;
    //    PlayerResources.instance.AddBlueberries(blueberries);
    //    blueberries -= blueberries;
    //    woodText.text = blueberries.ToString() + "/100";
    //    bar.value = blueberries;
    //}


    //public void AddGold(int gold)
    //{
    //    if (goldAmount + gold >= 100)
    //    {
    //        chopperBuilding.GetComponent<GoldMining>().enabled = false;
    //        goldAmount = 100;
    //        woodText.text = goldAmount.ToString() + "/100";
    //        bar.value = goldAmount;
    //    }
    //    else
    //    {
    //        this.goldAmount += gold;
    //        woodText.text = goldAmount.ToString() + "/100";
    //        bar.value = goldAmount;
    //    }
    //}

    //public void CollectGold()
    //{
    //    chopperBuilding.GetComponent<GoldMining>().enabled = true;
    //    PlayerResources.instance.AddGold(goldAmount);
    //    goldAmount -= goldAmount;
    //    woodText.text = goldAmount.ToString() + "/100";
    //    bar.value = goldAmount;
    //}




}
