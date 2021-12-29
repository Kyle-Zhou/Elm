using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuScript : MonoBehaviour
{

    //public GameObject workShopTemplate;
    public static BuildMenuScript instance;

    public GameObject forgeButtonGameObject;
    public GameObject anvilButtonGameObject;
    private Button forgeButton;
    private Button anvilButton;
    private Text forgeText;
    private Text anvilText;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;

        forgeButton = forgeButtonGameObject.GetComponent<Button>();
        anvilButton = anvilButtonGameObject.GetComponent<Button>();

        forgeText = forgeButtonGameObject.transform.GetChild(0).GetComponent<Text>();
        anvilText = anvilButtonGameObject.transform.GetChild(0).GetComponent<Text>();

    }

    public void BuyWorkShop() { //anvil
        if (PlayerResources.instance.GetWoodCount() >= 0 && PlayerResources.instance.GetRockCount() >= 5)
        {
            PlayerResources.instance.AddResource(-5, PlayerResources.instance.GetRockIndex());
            PlayerResources.instance.AddResourceToInventory(-5, PlayerResources.instance.GetRockIndex());

            anvilButton.interactable = false;
            anvilButton.enabled = false;
            anvilButtonGameObject.GetComponent<Image>().color = new Color32(90, 90, 90, 100);

            anvilText.text = "Anvil: 1/1";

        }
    }



    public void BuyChest()
    {
        if (PlayerResources.instance.GetWoodCount() >= 5)
        {
            PlayerResources.instance.AddResource(-5, PlayerResources.instance.GetWoodIndex());
            PlayerResources.instance.AddResourceToInventory(-5, PlayerResources.instance.GetWoodIndex());

            //anvilButton.interactable = false;
            //anvilButtonGameObject.GetComponent<Image>().color = new Color32(90, 90, 90, 100);


        }
    }

    public void BuyForge()
    {
        if (PlayerResources.instance.GetWoodCount() >= 0 && PlayerResources.instance.GetRockCount() >= 5)
        {
            PlayerResources.instance.AddResource(-5, PlayerResources.instance.GetRockIndex());
            PlayerResources.instance.AddResourceToInventory(-5, PlayerResources.instance.GetRockIndex());

            forgeButton.interactable = false;
            forgeButton.enabled = false;
            forgeButtonGameObject.GetComponent<Image>().color = new Color32(90, 90, 90, 100);

            forgeText.text = "Forge: 1/1";

        }
    }

    //public void BuyIronMine()
    //{
    //    if (PlayerResources.instance.GetWoodCount() >= 0 && PlayerResources.instance.GetRockCount() >= 0)
    //    {
    //        PlayerResources.instance.AddResource(-2, 0);
    //        //PlayerResources.instance.AddResourceToInventory(-2, 0); PlayerResources.instance.AddRocks(-5);

    //    }
    //}

    //public void BuyWoodChopper()
    //{
    //    if (PlayerResources.instance.GetWoodCount() >= 0 && PlayerResources.instance.GetRockCount() >= 0)
    //    {
    //        //PlayerResources.instance.AddResource(-2, 0);
    //        //PlayerResources.instance.AddResourceToInventory(-2, 0); PlayerResources.instance.AddRocks(-5);

    //    }
    //}


}
