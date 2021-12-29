using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtonPreset : MonoBehaviour
{

    public GameObject workshop1;

    public void MoveButtonClicked()
    {

        if (workshop1.CompareTag("WorkShop"))
        {

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
}
