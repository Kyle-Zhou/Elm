using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS SCRIPT DOES NOT ONLY DEAL WITH COLLISIONS BUT ALSO OPENING THE MENU AND DELETING

public class BuildingStates : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite selected;
    public Sprite passive;
    private GameObject menu;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { 
            sr.sprite = selected;
            OpenMenu();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sr.sprite = selected;
            OpenMenu();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sr.sprite = passive;
        }
    }


    private void OpenMenu()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false) {

                if (gameObject.CompareTag("Chest"))
                {
                    menu.GetComponent<ChestManager>().OpenChest();
                    //transform.GetChild(0).GetComponent<Animator>().SetBool("OpenChest", true);
                    transform.GetChild(0).GetComponent<Animator>().SetTrigger("OpenChest");
                } else if (gameObject.CompareTag("Anvil"))
                {
                    SoundManager.PlaySound(SoundManager.Sound.anvil);
                } else if (gameObject.CompareTag("Forge"))
                {
                    SoundManager.PlaySound(SoundManager.Sound.forgeOpen);
                }

                FreezePlayer.instance.SetFrozen(true);
                menu.SetActive(true);
            }
        }
    }


    public void SetWorkShopMenu(GameObject m) {
        menu = m;
    }

}
