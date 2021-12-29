using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{

    private SpriteRenderer sr;
    private BoxCollider2D boxCollider2D;
    public List<Sprite> sprites;
    private GameObject menu;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenMenu();
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenMenu();
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }

    private void OpenMenu()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false)
            {
                FreezePlayer.instance.SetFrozen(true);
                menu.SetActive(true);
            }
        }
    }

    public void GetMenu(GameObject m)
    {
        menu = m;
    }

    public void UpgradeTotemSprite()
    {
        sr.sprite = sprites[TotemManager.instance.GetCurrentTotemLevel()-1];
    }

}
