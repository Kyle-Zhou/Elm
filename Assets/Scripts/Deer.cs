using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MeleeMob
{

    private void OnEnable()
    {
        transform.GetChild(3).GetComponent<ParticleSystem>().Stop();
        StartCoroutine(LerpRevert());
        StartCoroutine("Wander"); //This has to be a string since the StopCoroutine() under Vision.cs that stops Wander() uses a string (stupid rule).
    }

    private void Start()
    {
        float randomMultiplier = Random.Range(0.0f, 0.6f) + 0.9f;
        int additionAmount = (int)(randomMultiplier * 10);
        maxHealth += additionAmount;
        health += additionAmount;

        transform.GetChild(0).localScale *= randomMultiplier;
        damage += additionAmount;

        lerpStretch *= randomMultiplier;
        lerpSquash *= randomMultiplier;
        standardSize *= randomMultiplier;

    }

    private void FixedUpdate()
    {
        if (attacking && !stunned)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * 1.5f * Time.fixedDeltaTime);
        }
        else if (!stunned)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public override void TakeDamage(int damage, Vector3 source)
    {
        health -= damage;
        animator.SetTrigger("Damage");
        this.GetComponent<Vision>().detected = true;
        StartCoroutine(Flinch());
        ResetMovement();
        rb.AddForce((transform.position - source).normalized * 150f); //This method of knockback kind of sucks; it's inaccurate.

        if (health <= 0)
        {
            GameObject deathExplode = Instantiate(deathExplosion, transform.position, Quaternion.identity);
            Destroy(deathExplode, 2f);

            //disable shadow
            transform.GetChild(2).gameObject.SetActive(false);
            dying = true;
            this.enabled = false;
            this.GetComponent<Vision>().enabled = false;
            animator.SetBool("Dead", true);
        }

        if (floatingText)
        {
            ShowFloatingText(damage);
        }

        GameObject hitEffect = Instantiate(explosion2, transform.position, Quaternion.identity);
        Destroy(hitEffect, 2f);
        GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explode, 2f);
    }

    public override void Attack()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attackRange, interactionLayer);

        foreach (Collider2D target in targets)
        {
            inRange = true;

            if (Time.time >= cooldown)
            {
                attacking = true;
                invulnerable = true;
                animator.SetTrigger("Attack");
                SetMovement(target.transform.position);
                cooldown = Time.time + 1f / attackRate;
            }

            if (!attacking)
            {
                ResetMovement();
            }
        }
    }

    private void EmitParticles()
    {
        transform.GetChild(3).GetComponent<ParticleSystem>().Emit(Random.Range(3, 7));
    }

}
