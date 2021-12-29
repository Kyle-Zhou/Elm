using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RenewableStates : ResourceStates
{
    public Sprite leftover;
    public GameObject digPrompt;
    public BoxCollider2D upperCollider;
    public GameObject bar;
    public Sprite[] bars;

    public GameObject treeShadow;
    public GameObject stumpShadow;

    public float timeOfDeath = 0, regrowthTime;
    public int maxHealth;
    public bool mature = true;

    private void Update()
    {
        if (timeOfDeath != 0)
        {
            if (Time.time - timeOfDeath > regrowthTime)
            {
                Grow();
                timeOfDeath = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool")) { 
            if (health > 0)
            {
                if (ToolInventory.instance.GetCurrentTool() == 1)
                {
                    sr.sprite = selected;
                }
                else if (ToolInventory.instance.GetCurrentTool() == 3)
                {
                    //sr.sprite = selected;
                    //digPrompt.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool"))
        {

            if (health > 0)
            {
                if (ToolInventory.instance.GetCurrentTool() == 1)
                {
                    sr.sprite = selected;
                }
                else if (ToolInventory.instance.GetCurrentTool() == 3)
                {
                    //sr.sprite = selected;
                    //digPrompt.SetActive(true);
                }
                else
                {
                    sr.sprite = passive;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool"))
        {
            if (health > 0)
            {
                sr.sprite = passive;
                //digPrompt.SetActive(false);

            }
            bar.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Grow()
    {
        upperCollider.enabled = true;
        mature = true;
        health = maxHealth;
        sr.sprite = passive;
        treeShadow.SetActive(true);
        stumpShadow.SetActive(false);
    }

    public void PromptM(bool on)
    {
        //gameObject.setactive(on);
    }

    public abstract void TakeHit(int damage);

    public abstract void MachineHit(int damage);
}
