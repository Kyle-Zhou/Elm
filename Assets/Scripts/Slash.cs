using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public int damage;
    public LayerMask vegLayer;
    public ContactFilter2D layer;
    private BoxCollider2D hitbox;

    private void Start()
    {
        hitbox = this.GetComponent<BoxCollider2D>();
    }

    private void Damage()
    {
        List<Collider2D> targets = new List<Collider2D>();
        Physics2D.OverlapCollider(hitbox, layer, targets);

        foreach (Collider2D collider in targets)
        {
            if (collider.gameObject.CompareTag("Mob"))
            {
                collider.gameObject.GetComponent<Mob>().TakeDamage(damage, transform.position);

                Debug.Log("mob");

            }
            if (collider.gameObject.CompareTag("Gatekeeper"))
            {
                collider.gameObject.GetComponent<Gatekeeper>().TakeDamage(damage, transform.position);
            }
            if (collider.gameObject.CompareTag("Vegetable"))
            {
                collider.gameObject.GetComponent<VegetableStates>().TakeHit(1);

                Debug.Log("veg");


            }
        }

        //Collider2D[] vegTargets = Physics2D.OverlapCircleAll(gameObject.transform.position, hitbox.edgeRadius, vegLayer);

        //foreach (Collider2D target in vegTargets)
        //{
        //    if (target.CompareTag("Vegetable"))
        //    {
        //        target.gameObject.GetComponent<VegetableStates>().TakeHit(1);
        //    }
        //}


    }

    private void Delete() {
        Destroy(gameObject);
    }
}
