using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Construction : MonoBehaviour
{
    public List<GameObject> buildings;
    public List<GameObject> menus;

    public GameObject buildingMenu;
    public Text insufficientFunds;

    public Transform canvas;
    public GameObject backpack;

    private int counter;

    public void SpawnWorkshop() //anvil
    {

        if (PlayerResources.instance.GetWoodCount() >= 0 && PlayerResources.instance.GetRockCount() >= 5) {

            counter++;

            buildingMenu.SetActive(false);

            FreezePlayer.instance.SetIsMovingBuilding(true);

            Vector2 spawn = new Vector2(0, 0);
            GameObject tempWorkshop = Instantiate(buildings[0], spawn, Quaternion.identity);
            tempWorkshop.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);


            tempWorkshop.name = tempWorkshop.name + counter;

            tempWorkshop.GetComponent<ContainerTemplateScript>().enabled = true;
            //GameObject tempWorkshopMenu = Instantiate(menus[0]);
            GameObject tempWorkshopMenu = menus[0];

            tempWorkshopMenu.transform.SetParent(canvas, false);

            tempWorkshop.GetComponent<ContainerTemplateScript>().SetWorkShopMenu(tempWorkshopMenu); //give template script reference to the menu instance to delete it if the workshop is not placed
            tempWorkshop.GetComponent<BuildingStates>().SetWorkShopMenu(tempWorkshopMenu); //give workshop collision script reference to workshop menu instance
                                                                                                // in order to open the menu

            tempWorkshopMenu.GetComponent<DynamicMoveButton>().SetWorkShop(tempWorkshop); //give the dynamic move button script a reference to the workshop instance
                                                                                          //tempWorkshopMenu.GetComponent<DynamicDeleteButton>().SetBuildingAndMenu(tempWorkshop, tempWorkshopMenu); //give delete button script reference to workshop and menu

        }
        else
        {
            StartCoroutine(FlashNoFundsText());
            FreezePlayer.instance.SetFrozen(false);
            FreezePlayer.instance.SetIsMovingBuilding(false);

        }
    }


    public void SpawnTreeChopper()
    {
        if (PlayerResources.instance.GetWoodCount() >= 0 && PlayerResources.instance.GetRockCount() >= 0)
        {

            buildingMenu.SetActive(false);

            FreezePlayer.instance.SetIsMovingBuilding(true);

            Vector2 spawn = new Vector2(0,0);
            GameObject tempTreeChopper = Instantiate(buildings[1], spawn, Quaternion.identity);
            tempTreeChopper.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);


            //tempTreeChopper.GetComponent<TemplateScript>().enabled = true;
            tempTreeChopper.GetComponent<ContainerTemplateScript>().enabled = true;
            GameObject tempTreeChopMenu = Instantiate(menus[1]);

            tempTreeChopMenu.transform.SetParent(canvas, false);

            tempTreeChopper.GetComponent<ContainerTemplateScript>().SetWorkShopMenu(tempTreeChopMenu); //give template script reference to the menu instance to delete it if the workshop is not placed
            // tempTreeChopper.GetComponent<TemplateScript>().SetWorkShopMenu(tempTreeChopMenu); //give template script reference to the menu instance to delete it if the workshop is not placed
            tempTreeChopper.GetComponent<BuildingStates>().SetWorkShopMenu(tempTreeChopMenu); //give workshop collision script reference to workshop menu instance
                                                                                                  // in order to open the menu
            tempTreeChopper.GetComponent<AutoChopping>().SetBuildingMenu(tempTreeChopMenu); //gives the autochopping script access to the menu

            //tempTreeChopMenu.GetComponent<DynamicMoveButton>().SetWorkShop(tempTreeChopper); //give the dynamic move button script a reference to the workshop instance
            //tempTreeChopMenu.GetComponent<DynamicDeleteButton>().SetBuildingAndMenu(tempTreeChopper, tempTreeChopMenu); //give delete button script reference to workshop and menu
            tempTreeChopMenu.GetComponent<DynamicButtons>().SetBuildingAndMenu(tempTreeChopper, tempTreeChopMenu);

            tempTreeChopMenu.GetComponent<CollectionSystem>().SetChopperBuilding(tempTreeChopper);

        } else
        {
            StartCoroutine(FlashNoFundsText());

            FreezePlayer.instance.SetFrozen(false);
            FreezePlayer.instance.SetIsMovingBuilding(false);

        }
    }

    public void SpawnGoldMine()
    {
        if (PlayerResources.instance.GetWoodCount() >= 0 && PlayerResources.instance.GetRockCount() >= 0)
        {

            buildingMenu.SetActive(false);


            FreezePlayer.instance.SetIsMovingBuilding(true);

            Vector2 spawn = new Vector2(0, 0);
            GameObject tempGoldMine = Instantiate(buildings[2], spawn, Quaternion.identity);
            tempGoldMine.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);


            tempGoldMine.GetComponent<ContainerTemplateScript>().enabled = true;
            GameObject tempGoldMineMenu = Instantiate(menus[2]);

            tempGoldMineMenu.transform.SetParent(canvas, false);

            tempGoldMine.GetComponent<ContainerTemplateScript>().SetWorkShopMenu(tempGoldMineMenu); //give template script reference to the menu instance to delete it if the workshop is not placed
            tempGoldMine.GetComponent<BuildingStates>().SetWorkShopMenu(tempGoldMineMenu); //give workshop collision script reference to workshop menu instance
                                                                                               // in order to open the menu
            tempGoldMineMenu.GetComponent<DynamicButtons>().SetBuildingAndMenu(tempGoldMine, tempGoldMineMenu);

            tempGoldMine.GetComponent<GoldMining>().SetBuildingMenu(tempGoldMineMenu);
            tempGoldMineMenu.GetComponent<CollectionSystem>().SetChopperBuilding(tempGoldMine);
        }
        else
        {
            StartCoroutine(FlashNoFundsText());

            FreezePlayer.instance.SetFrozen(false);
            FreezePlayer.instance.SetIsMovingBuilding(false);
        }
    }


    public void SpawnForge()
    {
        if (PlayerResources.instance.GetWoodCount() >= 0 && PlayerResources.instance.GetRockCount() >= 5)
        {

            buildingMenu.SetActive(false);

            FreezePlayer.instance.SetIsMovingBuilding(true);

            Vector2 spawn = new Vector2(0, 0);
            GameObject tempForge = Instantiate(buildings[3], spawn, Quaternion.identity);
            tempForge.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);



            tempForge.GetComponent<ContainerTemplateScript>().enabled = true;
            GameObject tempForgeMenu = menus[3];

            tempForgeMenu.transform.SetParent(canvas, false);

            tempForge.GetComponent<ContainerTemplateScript>().SetWorkShopMenu(tempForgeMenu); //give template script reference to the menu instance to delete it if the workshop is not placed
            tempForge.GetComponent<BuildingStates>().SetWorkShopMenu(tempForgeMenu); //give workshop collision script reference to workshop menu instance
                                                                                         // in order to open the menu

            tempForgeMenu.GetComponent<DynamicMoveButton>().SetWorkShop(tempForge); //give the dynamic move button script a reference to the workshop instance
            //tempForgeMenu.GetComponent<DynamicButtons>().SetBuildingAndMenu(tempForge, tempForgeMenu); //give delete button script reference to workshop and menu
        }
        else
        {
            StartCoroutine(FlashNoFundsText());
            FreezePlayer.instance.SetFrozen(false);
            FreezePlayer.instance.SetIsMovingBuilding(false);

        }
    }

    public void SpawnAutoPicker()
    {
        if (PlayerResources.instance.GetWoodCount() >= 0 && PlayerResources.instance.GetRockCount() >= 0)
        {

            buildingMenu.SetActive(false);

            FreezePlayer.instance.SetIsMovingBuilding(true);

            Vector2 spawn = new Vector2(0, 0);
            GameObject tempAutoPicker = Instantiate(buildings[4], spawn, Quaternion.identity);
            tempAutoPicker.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);

            //tempTreeChopper.GetComponent<TemplateScript>().enabled = true;
            tempAutoPicker.GetComponent<ContainerTemplateScript>().enabled = true;
            GameObject tempAutoPickerMenu = Instantiate(menus[4]);

            tempAutoPickerMenu.transform.SetParent(canvas, false);

            tempAutoPicker.GetComponent<ContainerTemplateScript>().SetWorkShopMenu(tempAutoPickerMenu); //give template script reference to the menu instance to delete it if the workshop is not placed
            // tempTreeChopper.GetComponent<TemplateScript>().SetWorkShopMenu(tempTreeChopMenu); //give template script reference to the menu instance to delete it if the workshop is not placed
            tempAutoPicker.GetComponent<BuildingStates>().SetWorkShopMenu(tempAutoPickerMenu); //give workshop collision script reference to workshop menu instance
                                                                                                 // in order to open the menu
            tempAutoPicker.GetComponent<AutoPicking>().SetBuildingMenu(tempAutoPickerMenu); //gives the autochopping script access to the menu

            //tempTreeChopMenu.GetComponent<DynamicMoveButton>().SetWorkShop(tempTreeChopper); //give the dynamic move button script a reference to the workshop instance
            //tempTreeChopMenu.GetComponent<DynamicDeleteButton>().SetBuildingAndMenu(tempTreeChopper, tempTreeChopMenu); //give delete button script reference to workshop and menu
            tempAutoPickerMenu.GetComponent<DynamicButtons>().SetBuildingAndMenu(tempAutoPicker, tempAutoPickerMenu);

            tempAutoPickerMenu.GetComponent<CollectionSystem>().SetChopperBuilding(tempAutoPicker);

        }
        else
        {
            StartCoroutine(FlashNoFundsText());

            FreezePlayer.instance.SetFrozen(false);
            FreezePlayer.instance.SetIsMovingBuilding(false);

        }
    }


    public void SpawnCookingPot()
    {
        if (PlayerResources.instance.GetWoodCount() >= 0 && PlayerResources.instance.GetRockCount() >= 0)
        {

            buildingMenu.SetActive(false);

            FreezePlayer.instance.SetIsMovingBuilding(true);

            Vector2 spawn = new Vector2(0, 0);
            GameObject tempPot = Instantiate(buildings[5], spawn, Quaternion.identity);
            tempPot.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);



            tempPot.GetComponent<TemplateScript>().enabled = true;
            GameObject tempPotMenu = Instantiate(menus[5]);

            tempPotMenu.transform.SetParent(canvas, false);

            tempPot.GetComponent<TemplateScript>().SetWorkShopMenu(tempPotMenu); //give template script reference to the menu instance to delete it if the workshop is not placed
            tempPot.GetComponent<BuildingStates>().SetWorkShopMenu(tempPotMenu); //give workshop collision script reference to workshop menu instance
                                                                                       // in order to open the menu


            //tempForgeMenu.GetComponent<DynamicMoveButton>().SetWorkShop(tempForge); //give the dynamic move button script a reference to the workshop instance
            tempPotMenu.GetComponent<DynamicButtons>().SetBuildingAndMenu(tempPot, tempPotMenu); //give delete button script reference to workshop and menu
        }
        else
        {
            StartCoroutine(FlashNoFundsText());
            FreezePlayer.instance.SetFrozen(false);
            FreezePlayer.instance.SetIsMovingBuilding(false);

        }
    }

    public void SpawnChest()
    {
        if (PlayerResources.instance.GetWoodCount() >= 5 && PlayerResources.instance.GetRockCount() >= 0)
        {

            buildingMenu.SetActive(false);

            FreezePlayer.instance.SetIsMovingBuilding(true);

            Vector2 spawn = new Vector2(0, 0);
            GameObject tempChest = Instantiate(buildings[6], spawn, Quaternion.identity);
            tempChest.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);



            tempChest.GetComponent<ContainerTemplateScript>().enabled = true;
            GameObject tempChestMenu = Instantiate(menus[6]);

            tempChestMenu.transform.SetParent(canvas, false);

            tempChestMenu.GetComponent<ChestManager>().SetInventory(backpack);

            tempChest.GetComponent<ContainerTemplateScript>().SetWorkShopMenu(tempChestMenu); //give template script reference to the menu instance to delete it if the workshop is not placed
            tempChest.GetComponent<ChestStates>().SetWorkShopMenu(tempChestMenu); //give workshop collision script reference to workshop menu instance
                                                                                     // in order to open the menu

            //tempForgeMenu.GetComponent<DynamicMoveButton>().SetWorkShop(tempForge); //give the dynamic move button script a reference to the workshop instance
            tempChestMenu.GetComponent<DynamicButtons>().SetBuildingAndMenu(tempChest, tempChestMenu); //give delete button script reference to workshop and menu
            tempChestMenu.GetComponent<ChestCloseButton>().SetInventory(backpack);
        }
        else
        {
            StartCoroutine(FlashNoFundsText());
            FreezePlayer.instance.SetFrozen(false);
            FreezePlayer.instance.SetIsMovingBuilding(false);

        }
    }


    IEnumerator FlashNoFundsText()
    {
        insufficientFunds.enabled = true;
        yield return new WaitForSeconds(1.0f);
        insufficientFunds.enabled = false;
    }


}
