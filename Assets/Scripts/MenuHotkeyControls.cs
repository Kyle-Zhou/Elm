using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHotkeyControls : MonoBehaviour
{

    private GameObject menu;
    private Animator animator;

    private void Start()
    {
        menu = gameObject;
        animator = menu.GetComponent<Animator>();
    }

    void Update()
    {
        if (menu.activeSelf){ 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menu.CompareTag("Chest"))
                {
                    animator.SetTrigger("MenuClose");
                    OpenBackpack.instance.CloseBackpack();
                    gameObject.GetComponent<ChestCloseButton>().CloseChestAnim();
                    SoundManager.PlaySound(SoundManager.Sound.chestOpen);


                }
                else if (menu.CompareTag("BackpackSlots"))
                {
                    SoundManager.PlaySound(SoundManager.Sound.backpackOpen);

                    animator.SetTrigger("MenuClose");
                    OpenBackpack.instance.CloseBackpack();
                } else if (menu.CompareTag("CookingPot"))
                {

                    SoundManager.PlaySound(SoundManager.Sound.potLipClose);
                    animator.SetTrigger("MenuClose");
                } else if (menu.CompareTag("Anvil")){
                    SoundManager.PlaySound(SoundManager.Sound.anvil);
                    animator.SetTrigger("MenuClose");
                } else if (menu.CompareTag("Forge"))
                {
                    SoundManager.PlaySound(SoundManager.Sound.forgeOpen);
                    animator.SetTrigger("MenuClose");
                }
                else { 

                    animator.SetTrigger("MenuClose");

                }
                //trigger a closing animation?

            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (menu.CompareTag("Chest"))
                {
                    GetComponent<DynamicButtons>().MoveButtonClicked();
                    gameObject.GetComponent<ChestCloseButton>().CloseChestAnim();
                    OpenBackpack.instance.CloseBackpack();

                } else if ((menu.CompareTag("Anvil")) || (menu.CompareTag("Forge")))
                {
                    GetComponent<DynamicMoveButton>().ButtonClicked(); 
                } else if ((menu.CompareTag("CookingPot")) || (menu.CompareTag("BuildMenu")))
                {
                    GetComponent<MoveButtonPreset>().MoveButtonClicked();
                }
            }
        }
    }

    public void Close()
    {
        menu.SetActive(false);
        FreezePlayer.instance.SetFrozen(false);
        FreezePlayer.instance.SetHoverOverButton(false);
    }

  

}
