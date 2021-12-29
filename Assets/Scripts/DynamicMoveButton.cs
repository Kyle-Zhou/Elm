using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicMoveButton : MonoBehaviour
{

    public Button moveButton;
    private GameObject workshop1;

    //call this method in building spawner (basically start)
    public void SetWorkShop(GameObject workshop)
    {
        workshop1 = workshop;
        moveButton.onClick.AddListener(() => ButtonClicked());
    }

    public void ButtonClicked()
    {

        workshop1.GetComponent<ContainerMovementScript>().enabled = true;

        workshop1.GetComponent<ContainerMovementScript>().SetPlaced(false);

        workshop1.GetComponent<ContainerMovementScript>().SetInitalPosition();

        gameObject.SetActive(false);
    }
}
