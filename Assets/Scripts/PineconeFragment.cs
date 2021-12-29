using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineconeFragment : Projectile
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBar.instance.SubtractHealth(source.GetComponent<BlightedPine>().damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("WallTilemap"))
        {
            Destroy(gameObject);
        }
    }
}
