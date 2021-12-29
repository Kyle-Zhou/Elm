using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob")) {
            if (!collision.GetComponent<Mob>().invulnerable && !collision.GetComponent<Mob>().dying)
            {
                collision.GetComponent<Mob>().TakeDamage(damage, transform.position);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("WallTilemap"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Gatekeeper"))
        {
            collision.GetComponent<Gatekeeper>().TakeDamage(damage, transform.position);
            Destroy(gameObject);
        }
    }
}
