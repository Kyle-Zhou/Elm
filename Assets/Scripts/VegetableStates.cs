using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VegetableStates : ResourceStates
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool"))
        {
            sr.sprite = selected;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool"))
        {
            sr.sprite = selected;   
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool"))
        {
            
             sr.sprite = passive;
            
        }
    }

    public abstract void TakeHit(int damage);

    public abstract void MachineHit(int damage);
}
