using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planting : MonoBehaviour
{
    public GameObject normalTreeSapling;
    public GameObject cherryTree;
    public GameObject inventoryMenu;

    public static Planting instance;

    public void Start()
    {
        instance = this;
    }

    public void SpawnNormalTreeSapling()
    {
        FreezePlayer.instance.SetIsMovingBuilding(true);
        OpenBackpack.instance.CloseBackpack();

        //Vector2 spawn = new Vector2(0, 0);
        GameObject tempNormalTree = Instantiate(normalTreeSapling);
        tempNormalTree.GetComponent<ContainerTemplateScript>().enabled = true;

        PlayerResources.instance.AddResource(-1, 10);
        PlayerResources.instance.AddResourceToInventory(-1, 10);


    }


    //change this to growing blueberry bushes (but probably wanna replace this with farming stuff)
    //public void SpawnCherryTree()
    //{
    //    //check if the necessary resources are here
    //    inventoryMenu.SetActive(false);

    //    FreezePlayer.instance.SetIsMovingBuilding(true);

    //    Vector2 spawn = new Vector2(0, 0);
    //    GameObject tempCherryTree = Instantiate(cherryTree, spawn, Quaternion.identity);
    //    tempCherryTree.GetComponent<ContainerTemplateScript>().enabled = true;
    //}

}
