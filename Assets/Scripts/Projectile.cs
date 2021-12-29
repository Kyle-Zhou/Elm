using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public float startTime;
    public float lifeTime = 4f;
    public GameObject source;

    private void Start()
    {
        startTime = Time.time;
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        if (Time.time > startTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
