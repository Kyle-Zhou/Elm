using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChestCloseButton : MonoBehaviour
{

    //public static ChestCloseButton instance;

    public Button closeButton; //
    public Button moveButton;
    private GameObject chest;


    //public void Start()
    //{
    //    instance = this;
    //}

    public void SetInventory(GameObject backpack)
    {
        closeButton.onClick.AddListener(() => DisableInventory(backpack));
        moveButton.onClick.AddListener(() => DisableInventory(backpack));
    }

    public void DisableInventory(GameObject backpack)
    {

        OpenBackpack.instance.CloseBackpack();
        backpack.transform.Find("Panel_SideMenu_Close").GetChild(1).gameObject.SetActive(true);
        backpack.transform.transform.Find("Panel_SideMenu_Close").GetChild(2).gameObject.SetActive(true);

        CloseChestAnim();
        gameObject.SetActive(false);
    }

    public void CloseChestAnim()
    {
        chest.transform.GetChild(0).GetComponent<Animator>().SetTrigger("CloseChest");
    }

    public void SetBuilding(GameObject b)
    {
        chest = b;
    }

}
