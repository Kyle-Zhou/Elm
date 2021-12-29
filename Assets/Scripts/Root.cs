using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public GameObject source;
    public Collider2D hitbox;
    public ParticleSystem ps;
    public Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        ps.Stop();
        hitbox.enabled = false;
    }

    private void EmitParticles()
    {
        ps.Emit(5);
    }

    private void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    private void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void Delete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBar.instance.SubtractHealth(source.GetComponent<BlightedPine>().damage);
        }
    }
}
