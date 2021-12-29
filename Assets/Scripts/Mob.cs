using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    public int maxHealth, health, damage;
    public float moveSpeed, cooldown = 0, attackRate, attackRange;
    public bool rightward, stunned, inRange, invulnerable, attacking, dying;
    public List<Drop> drops;

    public float wanderDelay1, wanderDelay2, wanderDelay3, wanderDelay4;

    public GameObject floatingText;
    public GameObject deathExplosion, explosion, explosion2;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator animator;
    public LayerMask interactionLayer;
    public Vector2 movement;

    public Vector3 lerpStretch;
    public Vector3 lerpSquash;
    public Vector3 standardSize;

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(movement.sqrMagnitude));

        if (movement.x < 0 && !this.GetComponent<Vision>().detected)
        {
            rightward = false;
            sr.flipX = true;
        }
        else if (movement.x > 0 && !this.GetComponent<Vision>().detected)
        {
            rightward = true;
            sr.flipX = false;
        }

        inRange = false;

        if (this.GetComponent<Vision>().check)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (!stunned)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public abstract void TakeDamage(int damage, Vector3 source);

    public void ShowFloatingText(int damage)
    {
        var x = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        x.GetComponent<TextMesh>().text = damage.ToString();
    }

    public abstract void Attack();

    public void Deal()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 1, interactionLayer);

        foreach (Collider2D target in targets)
        {
            HealthBar.instance.SubtractHealth(damage);
        }

        ResetMovement();
    }

    public void SetMovement(Vector2 destination)
    {
        movement = (destination - rb.position).normalized;
    }

    public void ResetMovement()
    {
        movement = new Vector2(0, 0);
    }

    private void ClearAttackState()
    {
        attacking = false;
        invulnerable = false;
    }

    public void Stretch()
    {
        transform.localScale = lerpStretch;
    }

    public void Squash()
    {
        transform.localScale = lerpSquash;
    }

    public IEnumerator LerpRevert()
    {
        while (true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, standardSize, 0.2f);
            yield return null;
        }
    }

    public void Drops()
    {
        float x = Random.Range(gameObject.transform.position.x + 0.3f, gameObject.transform.position.x - 0.3f);
        float y = Random.Range(gameObject.transform.position.y + 0.3f, gameObject.transform.position.y - 0.3f);
        Vector3 spawn = new Vector2(x, y);

        int Amount = Random.Range(1,3);

        for (int i = 0; i < Amount; i++) {
            Instantiate(drops[0], spawn, transform.rotation);
        }
    }

    private IEnumerator Disintegrate()
    {
        for (float i = 1; i >= 0; i -= 0.01f)
        {
            sr.color = new Color(1, 1, 1, i);
            yield return null;
        }

        Destroy(gameObject);
    }

    public IEnumerator Flinch()
    {
        stunned = true;
        yield return new WaitForSeconds(0.4f);
        stunned = false;
    }

    public IEnumerator Wander() //Can further randomize values.
    {
        while (true)
        {
            Vector2 destination = (Vector2)transform.position + Random.insideUnitCircle;
            //yield return new WaitForSeconds(Random.Range(2f, 4f));
            yield return new WaitForSeconds(Random.Range(wanderDelay1, wanderDelay2));
            SetMovement(destination);
            //yield return new WaitForSeconds(Random.Range(0.6f, 1.2f));
            yield return new WaitForSeconds(Random.Range(wanderDelay3, wanderDelay4));
            ResetMovement();
        }
    }
}
