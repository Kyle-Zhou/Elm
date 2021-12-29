using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingChest : MonoBehaviour
{

    public GameObject startingChestMenu;
    public GameObject backpack;

    public List <GameObject> startingItems;

    private int amount = 15;

    // Start is called before the first frame update
    void Start()
    {

        startingChestMenu.GetComponent<ChestManager>().SetInventory(backpack);

        gameObject.GetComponent<ContainerTemplateScript>().SetWorkShopMenu(startingChestMenu); //give template script reference to the menu instance to delete it if the workshop is not placed
        gameObject.GetComponent<ChestStates>().SetWorkShopMenu(startingChestMenu); //give workshop collision script reference to workshop menu instance
                                                                                   // in order to open the menu

        //tempForgeMenu.GetComponent<DynamicMoveButton>().SetWorkShop(tempForge); //give the dynamic move button script a reference to the workshop instance
        startingChestMenu.GetComponent<DynamicButtons>().SetBuildingAndMenu(gameObject, startingChestMenu); //give delete button script reference to workshop and menu
        startingChestMenu.GetComponent<ChestCloseButton>().SetInventory(backpack);

        for (int i = 0; i < startingItems.Count; i++)
        {

            GameObject resourceItem = Instantiate(startingItems[i]);
            RectTransform rt = resourceItem.GetComponent<RectTransform>();

            resourceItem.GetComponent<Item>().AddResourceAmount(amount);

            if (resourceItem.GetComponent<Item>().GetResourceAmount() > 0)
            {
                startingChestMenu.GetComponent<ChestManager>().SetSlot(resourceItem);
            }


        }

        //startingChestMenu.GetComponent<ChestResources>().AddResource(amount, PlayerResources.instance.GetWoodIndex());
        //startingChestMenu.GetComponent<ChestResources>().AddResource(amount, PlayerResources.instance.GetRockIndex());
        //startingChestMenu.GetComponent<ChestResources>().AddResource(amount, PlayerResources.instance.GetBlueberryIndex());
        //startingChestMenu.GetComponent<ChestResources>().AddResource(amount, PlayerResources.instance.GetWheatIndex());
        //startingChestMenu.GetComponent<ChestResources>().AddResource(amount, PlayerResources.instance.GetCarrotIndex());


    }


}
