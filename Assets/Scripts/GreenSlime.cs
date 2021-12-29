using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : MeleeMob
{

    private Vector3 jiggle1, jiggle2, jiggle3;

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

        jiggle1 = new Vector3(0.6f, 1.4f, 1) * randomMultiplier;
        jiggle2 = new Vector3(1.3f, 0.7f, 1) * randomMultiplier;
        jiggle3 = new Vector3(0.9f, 1.1f, 1) * randomMultiplier;
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
        if (!dying)
        {
            health -= damage;
            StartCoroutine(Jiggle());
            animator.SetTrigger("Damage");

            this.GetComponent<Vision>().detected = true;
            StartCoroutine(Flinch());
            ResetMovement();
            rb.AddForce((transform.position - source).normalized * 150f); //This method of knockback kind of sucks; it's inaccurate.

            if (health <= 0)
            {
                SoundManager.PlaySound(SoundManager.Sound.EnemyDeath);

                GameObject deathExplode = Instantiate(deathExplosion, transform.position, Quaternion.identity);
                Destroy(deathExplode, 2f);

                Drops();

                //disable shadow
                transform.GetChild(2).gameObject.SetActive(false);
                dying = true;
                this.enabled = false;
                this.GetComponent<Vision>().enabled = false;
                animator.SetBool("Dead", true);
            } else
            {
                SoundManager.PlaySound(SoundManager.Sound.EnemyHit);
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

    private IEnumerator Jiggle()
    {
        transform.localScale = jiggle1;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = jiggle2;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = jiggle3;
    }
}
