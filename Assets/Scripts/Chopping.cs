using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopping : ToolFunction
{
    private void Start()
    {
        useRate = 1.5f;
        hitbox.radius = useRange;
        damage = 20;
        trigger = "Chop";
    }

    public override void Deal()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(hitboxCentre.position, useRange, interactionLayer);

        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("NormalTree"))
            {
                target.GetComponent<TreeStates>().TakeHit(1);
            }
            else if (target.CompareTag("Mob"))
            {
                if (!target.GetComponent<Mob>().invulnerable && !target.GetComponent<Mob>().dying)
                {
                    target.GetComponent<Mob>().TakeDamage(damage, hitboxCentre.position);
                }
            } else if (target.CompareTag("Vegetable"))
            {
                target.GetComponent<VegetableStates>().TakeHit(1);
            }
        }
    }

    public void SetDamage(int setDamage)
    {
        damage = setDamage;
    }
}

