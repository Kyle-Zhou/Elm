using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicButtons : MonoBehaviour
{
    public Button moveButton;
    public Button deleteButton;

    private GameObject workshop1;

    //call this method in Construction (basically start)
    public void SetBuildingAndMenu(GameObject building, GameObject menu)
    {
        deleteButton.onClick.AddListener(() => DeleteButtonClicked(building, menu));
        moveButton.onClick.AddListener(() => MoveButtonClicked());

        //give closing script access to the chest game object
        gameObject.GetComponent<ChestCloseButton>().SetBuilding(building);

        workshop1 = building;

    }

    public void MoveButtonClicked()
    {

        if (workshop1.CompareTag("WorkShop")) {

        workshop1.GetComponent<MovingBuilding>().enabled = true;

        workshop1.GetComponent<MovingBuilding>().SetPlaced(false);

        workshop1.GetComponent<MovingBuilding>().SetInitalPosition();
        } 
        else
        {
            workshop1.GetComponent<ContainerMovementScript>().enabled = true;
            workshop1.GetComponent<ContainerMovementScript>().SetPlaced(false);

            workshop1.GetComponent<ContainerMovementScript>().SetInitalPosition();
        }

        gameObject.SetActive(false);
    }


    public void DeleteButtonClicked(GameObject building, GameObject menu)
    {

        //if (building.CompareTag("building name")) ....
        //PlayerResources.instance.AddRocks(5);
        //PlayerResources.instance.AddWood(5);
        //refund or no???

        if (building.CompareTag("Chest"))
        {
            if (gameObject.GetComponent<ChestManager>().CheckEmpty() == true)
            {
                FreezePlayer.instance.SetFrozen(false);
                OpenBackpack.instance.CloseBackpack();

                Destroy(building);
                Destroy(menu);
            } else
            {
                //display warning message;
            }

        } else { 
            FreezePlayer.instance.SetFrozen(false);

            Destroy(building);
            Destroy(menu);
        }
    }

}
