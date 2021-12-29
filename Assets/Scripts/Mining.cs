using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : ToolFunction
{
    private void Start()
    {
        useRate = 1.5f;
        hitbox.radius = useRange;
        damage = 15;
        trigger = "Mine";
    }

    public override void Deal()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(hitboxCentre.position, useRange, interactionLayer);

        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("Rock"))
            {
                target.GetComponent<RockStates>().TakeHit(1);
            }
            else if (target.CompareTag("Mob"))
            {
                if (!target.GetComponent<Mob>().invulnerable && !target.GetComponent<Mob>().dying)
                {
                    target.GetComponent<Mob>().TakeDamage(damage, hitboxCentre.position);
                }
            }
            else if (target.CompareTag("Vegetable"))
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

