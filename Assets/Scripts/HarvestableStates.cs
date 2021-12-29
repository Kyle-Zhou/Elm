using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class HarvestableStates : ResourceStates
{
    public Sprite leftover;
    public Sprite selectedNaked;

    public SpriteRenderer harvestPrompt, digPrompt;

    public float timeOfHarvest = 0f, regrowthTime;
    public bool mature = true;

    private void Update()
    {
        if (timeOfHarvest != 0)
        {
            if (Time.time - timeOfHarvest > regrowthTime)
            {
                Grow();
                timeOfHarvest = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool"))
        {
            if (mature)
            {
                //digPrompt.enabled = false;
                sr.sprite = selected;
                harvestPrompt.enabled = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool"))
        {

            if (mature)
            {
                sr.sprite = selected;
                harvestPrompt.enabled = true;
            } else if (!mature && ToolInventory.instance.GetCurrentTool() == 3)
            {
                sr.sprite = selectedNaked;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Collect();
                harvestPrompt.enabled = false;
                //digPrompt.enabled = false;
            }


            //if (ToolInventory.instance.GetCurrentTool() == 3)
            //{
            //    harvestPrompt.enabled = false;
            //    //digPrompt.enabled = true;

            //}
            //else
            //{
            //    //digPrompt.enabled = false;
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Tool"))
        {
            harvestPrompt.enabled = false;
            //digPrompt.enabled = false;

            if (mature)
            {
                sr.sprite = passive;
            } else if (!mature)
            {
                sr.sprite = leftover;
            }
        }
    }

    void Grow()
    {
        mature = true;
        sr.sprite = passive;
    }

    public abstract void Collect();

    public abstract void MachineCollect();
}
