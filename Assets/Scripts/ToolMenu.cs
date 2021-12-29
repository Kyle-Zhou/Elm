using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolMenu : MonoBehaviour
{

    public List<GameObject> toolSprites;
    public GameObject checkMark;
    public int toolIndex;
    public List<GameObject> recipes;
    public List<GameObject> r1;
    public List<GameObject> r2;

    public void Start()
    {
        //PlayerResources.instance.AddResource(30, 0); //wood
        //PlayerResources.instance.AddResourceToInventory(30, 0);
        //PlayerResources.instance.AddResource(20, 2); //plat
        //PlayerResources.instance.AddResourceToInventory(20, 2);
        //PlayerResources.instance.AddResource(20, 15); //slime
        //PlayerResources.instance.AddResourceToInventory(20, 15);
    }

    public void OnEnable()
    {
        //ToolUpgrades.instance.UpgradeTools(3);
        toolIndex = 3;
        if (ToolUpgrades.instance.GetToolLevel(toolIndex) == 1)
        {
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

    public void UpdateSprites(int index) { 

        checkMark.GetComponent<RectTransform>().anchoredPosition = toolSprites[index].GetComponent<RectTransform>().anchoredPosition;

        toolSprites[index].GetComponent<Image>().color = Color.white;
    }

    public void UgradeTool()
    { 

        if (ToolUpgrades.instance.GetToolLevel(toolIndex) == 1) { 
            if (Upgrade1Possible()) { 
                ToolUpgrades.instance.UpgradeTools(toolIndex);
                UpdateSprites(ToolUpgrades.instance.GetToolLevel(toolIndex) - 1);

                foreach (GameObject recipe in recipes)
                {
                    recipe.SetActive(false);
                }


                //-------THIS PART ENABLES THE NEXT RECIPE IN LINE AFTER THE UPGRADE-----------

                //recipes[ToolUpgrades.instance.GetToolLevel(toolIndex)-1].SetActive(true);

                //foreach (GameObject item in r2)
                //{
                //    Text t = item.transform.GetChild(0).gameObject.GetComponent<Text>();
                //    int amountOfItem = PlayerResources.instance.GetResourceCount(item.GetComponent<TotemItem>().index);
                //    int necessaryAmount = item.GetComponent<TotemItem>().necessaryAmount;
                //    if (amountOfItem >= necessaryAmount) amountOfItem = necessaryAmount;
                //    t.text = amountOfItem.ToString() + " / " + necessaryAmount.ToString();
                //}

            }
        }
        //else if == 2
    }

    public bool Upgrade1Possible()
    {
        if (PlayerResources.instance.GetWoodCount() >= 10)
        {
            if (PlayerResources.instance.GetPlatCount() >= 10)
            {
                if (PlayerResources.instance.GetSlimeCount() >= 10) { 
                    Buy1();
                    return true;
                }
            }
        }
        return false;
    }
    public void Buy1()
    {
        SoundManager.PlaySound(SoundManager.Sound.anvil);
        PlayerResources.instance.AddResource(-10, 0); //wood
        PlayerResources.instance.AddResourceToInventory(-10, 0);
        PlayerResources.instance.AddResource(-10, 2); //plat
        PlayerResources.instance.AddResourceToInventory(-10, 2);
        PlayerResources.instance.AddResource(-10, 15); //slime
        PlayerResources.instance.AddResourceToInventory(-10, 15);
    }


}
