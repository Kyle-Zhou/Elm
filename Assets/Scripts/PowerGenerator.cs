using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{


    public GameObject powerGeneratorMenu;
    public Transform canvas;


    private void Start()
    {
        //GameObject tempPowerGenerator = Instantiate(powerGeneratorMenu);
        //tempPowerGenerator.transform.SetParent(canvas, false);

        gameObject.GetComponent<BuildingStates>().SetWorkShopMenu(powerGeneratorMenu);
        gameObject.GetComponent<PowerGenerator>().enabled = false;
    }





}
