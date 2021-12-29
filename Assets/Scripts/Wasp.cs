using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : RangedMob
{
    private void OnEnable()
    {
        StartCoroutine(LerpRevert());
        StartCoroutine("Wander"); //This has to be a string since the StopCoroutine() under Vision.cs that stops Wander() uses a string (stupid rule).
    }

    public override void Attack()
    {
        targets = Physics2D.OverlapCircleAll(transform.position, attackRange, interactionLayer);
        
        if (targets.Length != 0)
        {
            ResetMovement();
            inRange = true;
            destination = targets[0].transform.position;

            if (Time.time >= cooldown)
            {
                attacking = true;
                animator.SetTrigger("Shoot");
                cooldown = Time.time + 1f / attackRate;
            }
        }
    }

    public override void Shoot()
    {
        if (targets.Length != 0)
        {
            GameObject g = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 trajectory = (targets[0].transform.position - transform.position).normalized;
            g.transform.eulerAngles = new Vector3(0, 0, CalculateAngle(trajectory));
        }
        else
        {
            GameObject g = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 trajectory = (destination - transform.position).normalized;
            g.transform.eulerAngles = new Vector3(0, 0, CalculateAngle(trajectory));
        }
    }

    public override void TakeDamage(int damage, Vector3 source)
    {
        if (!dying)
        {
            health -= damage;
            Squash();
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
}
