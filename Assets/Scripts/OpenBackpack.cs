using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBackpack : MonoBehaviour
{
    //
    public static OpenBackpack instance;

    public GameObject panel_SideMenu_CloseButton;
    public GameObject backpackSlots;
    public GameObject backpackMats;
    public GameObject mark;
    private Image backpackImg;

    private bool backpackOn;

    void Start()
    {
        instance = this;
        backpackImg = backpackMats.transform.GetChild(0).GetComponent<Image>(); //doesnt work need fix
        backpackOn = false;
    }

    public void TurnOnBackpack()
    {
        panel_SideMenu_CloseButton.SetActive(true);
        backpackSlots.SetActive(true);
        backpackImg.enabled = true;
        backpackOn = true;
        FreezePlayer.instance.SetHoverOverButton(true);
    }

    public void CloseBackpack()
    {
        panel_SideMenu_CloseButton.SetActive(false);
        mark.SetActive(false);
        backpackSlots.SetActive(false);
        backpackImg.enabled = false;
        backpackOn = false;
        FreezePlayer.instance.SetHoverOverButton(false);

    }

    public bool GetBackpackOn() {
        return backpackOn;
    }

}
