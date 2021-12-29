using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicDeleteButton : MonoBehaviour
{

    public Button deleteButton;


    //call this method in building spawner (basically start)
    public void SetBuildingAndMenu(GameObject building, GameObject menu)
    {
        deleteButton.onClick.AddListener(() => ButtonClicked(building, menu));
    }

    public void ButtonClicked(GameObject building, GameObject menu)
    {
        //refund
       // PlayerResources.instance.AddRocks(5);
       // PlayerResources.instance.AddWood(5);
        FreezePlayer.instance.SetFrozen(false);

        Destroy(building);
        Destroy(menu);   
    }

}
