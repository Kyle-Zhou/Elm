using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractable : MonoBehaviour
{

    public static ButtonInteractable instance;
    public Button buildButton;
    public Button backpackButton;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (FreezePlayer.instance.GetHasBeenFrozen() == true) //what if i just put this in freezeplayer
        {
            SetButtonInteraction(false);
        } else
        {
            SetButtonInteraction(true);
        }
    }

    public void SetButtonInteraction(bool interactable)
    {
        buildButton.interactable = interactable;
        backpackButton.interactable = interactable;

    }


}
