using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS SCRIPT DOES NOT ONLY DEAL WITH COLLISIONS BUT ALSO OPENING THE MENU AND DELETING

public class ChestStates : MonoBehaviour
{
    //public SpriteRenderer sr;
    //public Sprite selected;
    //public Sprite passive;
    private GameObject menu;
    private GameObject childChest;
    private Animator animator;

    private void Start()
    {
        childChest = transform.GetChild(0).gameObject;
        animator = childChest.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenMenu();
            animator.SetBool("ChestSelected", true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenMenu();
            animator.SetBool("ChestSelected", true);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("ChestSelected", false);

        }
    }


    private void OpenMenu()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false)
            {

                if (gameObject.CompareTag("Chest"))
                {
                    menu.GetComponent<ChestManager>().OpenChest();
                    //transform.GetChild(0).GetComponent<Animator>().SetBool("OpenChest", true);
                    transform.GetChild(0).GetComponent<Animator>().SetTrigger("OpenChest");
                    SoundManager.PlaySound(SoundManager.Sound.chestOpen);

                }

                FreezePlayer.instance.SetFrozen(true);
                menu.SetActive(true);
            }
        }
    }


    public void SetWorkShopMenu(GameObject m)
    {
        menu = m;
    }

}
