using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : Projectile
{
    private void Start()
    {
        transform.GetChild(0).transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, -45);
        startTime = Time.time;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBar.instance.SubtractHealth(source.GetComponent<RangedMob>().damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("WallTilemap"))
        {
            Destroy(gameObject);
        }
    }
}
