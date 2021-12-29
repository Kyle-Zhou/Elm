using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS SCRIPT DOES NOT ONLY DEAL WITH COLLISIONS BUT ALSO OPENING THE MENU AND DELETING

public class CraftingTableStates : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite selected;
    public Sprite passive;
    public GameObject menu;


    //public void Update()
    //{
    //    SetParentToRoom();
    //    enabled = false;
    //}

    public void SetParentToRoom()
    {
        gameObject.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);
    }

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
        if (Input.GetMouseButtonDown(0))
        {
            if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false)
            {
                FreezePlayer.instance.SetFrozen(true);
                SoundManager.PlaySound(SoundManager.Sound.hammerDrop);
                menu.SetActive(true);
            }
        }
    }



}
