using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS SCRIPT DOES NOT ONLY DEAL WITH COLLISIONS BUT ALSO OPENING THE MENU AND DELETING

public class CookingPotStates : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite selected;
    public Sprite passive;
    private Animator animator;
    public GameObject menu1;
    public GameObject menu2;
    public GameObject progressBarFill;

    //public void Update()
    //{
    //    SetParentToRoom();
    //    enabled = false;
    //}
    //

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetParentToRoom()
    {
        gameObject.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (progressBarFill.GetComponent<RadialProgressBar>().GetRun() == false) { 
                //sr.sprite = selected;
                animator.SetBool("Selected", true);
                if (menu2.GetComponent<CollectFood>().GetFoodCollected() == true) { 
                    OpenMenu();
                } else if (menu2.GetComponent<CollectFood>().GetFoodCollected() == false)
                {
                    OpenMenu2();
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (progressBarFill.GetComponent<RadialProgressBar>().GetRun() == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //sr.sprite = selected;
                animator.SetBool("Selected", true);

                if (menu2.GetComponent<CollectFood>().GetFoodCollected() == true)
                {
                    OpenMenu();
                }
                else if (menu2.GetComponent<CollectFood>().GetFoodCollected() == false)
                {
                    OpenMenu2();
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //sr.sprite = passive;
            if (progressBarFill.GetComponent<RadialProgressBar>().GetRun() == false)
            {
                animator.SetBool("Selected", false);

            }
        }
    }


    private void OpenMenu()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false)
            {
                FreezePlayer.instance.SetFrozen(true);
                menu1.SetActive(true);
                SoundManager.PlaySound(SoundManager.Sound.potLidOpen);
            }
        }
    }
    private void OpenMenu2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false)
            {
                FreezePlayer.instance.SetFrozen(true);
                menu2.SetActive(true);
                SoundManager.PlaySound(SoundManager.Sound.potLidOpen);
            }
        }
    }


}
