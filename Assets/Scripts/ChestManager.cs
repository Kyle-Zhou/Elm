using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{

    public GameObject[] slots;
    private GameObject backpack;
    private GameObject sideMenu;
    private GameObject backPackCloseButton;

    void Start()
    {
        //backpack.SetActive(true);
        //sideMenu = backpack.transform.Find("SideMenu").gameObject;
        //sideMenu.SetActive(false);
        //backPackCloseButton = backpack.transform.Find("Close").gameObject;
        //backPackCloseButton.SetActive(false);
    }

    public void OpenChest()
    {
        //backpack.SetActive(true); //

        OpenBackpack.instance.TurnOnBackpack();

        sideMenu = backpack.transform.Find("Panel_SideMenu_Close").GetChild(1).gameObject;
        sideMenu.SetActive(false);
        backPackCloseButton = backpack.transform.Find("Panel_SideMenu_Close").GetChild(2).gameObject;
        backPackCloseButton.SetActive(false);
    }

    public void SetSlot(GameObject button)
    {
        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (slots[i].transform.childCount <= 0)
            {
                //Debug.Log(slots[i].name);
                button.transform.SetParent(slots[i].transform);
                button.transform.position = slots[i].transform.position;
            }

        }
    }

    public void RemoveSlot(GameObject button)
    {
        button.transform.parent = null;
    }


    public int DroppedOnSlot(GameObject slot)
    {
        if (slot.transform.childCount <= 0)
        {
            for (int i = slots.Length - 1; i >= 0; i--)
            {
                if (slots[i].name == slot.name)
                {
                    return i;
                }

            }
        }
        if (slot.transform.childCount >= 1)
        {
            for (int i = slots.Length - 1; i >= 0; i--)
            {
                if (slots[i].name == slot.name)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public bool CheckEmpty()
    {
        bool empty = true;

        foreach(GameObject slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                empty = false;
            }
        }
        return empty;
    }

    public void SetInventory(GameObject inv)
    {
        backpack = inv;
    }


}