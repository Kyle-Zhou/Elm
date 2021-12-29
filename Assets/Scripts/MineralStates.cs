using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MineralStates : ResourceStates
{
    public GameObject bar;
    public Sprite[] bars;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool"))
        {
            if (health > 0)
            {
                if (ToolInventory.instance.GetCurrentTool() == 2)
                {
                    sr.sprite = selected;
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
                if (ToolInventory.instance.GetCurrentTool() == 2)
                {
                    sr.sprite = selected;
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
            }
        }
    }

    public abstract void TakeHit(int damage);

    public abstract void MachineHit(int damage);
}
