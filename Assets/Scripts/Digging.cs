using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : ToolFunction
{
    private void Start()
    {
        useRate = 1f;
        useRange = 0.25f;
        hitbox.radius = useRange;
        damage = 10;
        trigger = "Dig";
    }

    public override void Deal()
    {
        //Collider2D[] targets = Physics2D.OverlapCircleAll(hitboxCentre.position, useRange, interactionLayer);

        //bool onlyOneItem = true;

        //foreach (Collider2D target in targets)
        //{
        //    if (onlyOneItem)
        //    {
        //        if (target.CompareTag("NormalTree"))
        //        {
        //            target.gameObject.GetComponent<ContainerMovementScript>().enabled = true;
        //            target.gameObject.GetComponent<ContainerMovementScript>().SetPlaced(false);
        //            onlyOneItem = false;

        //        }
        //        else if (target.CompareTag("BlueberryBush"))
        //        {
        //            target.gameObject.GetComponent<ContainerMovementScript>().enabled = true;
        //            target.gameObject.GetComponent<ContainerMovementScript>().SetPlaced(false);
        //            onlyOneItem = false;
        //        }
        //        else if (target.CompareTag("Vegetable"))
        //        {
        //            target.GetComponent<VegetableStates>().TakeHit(1);
        //        }
        //    }
        //}
    }
}
