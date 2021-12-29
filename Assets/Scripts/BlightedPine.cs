using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlightedPine : Gatekeeper
{
    private int random;
    private float decay;
    public Rigidbody2D player;
    public GameObject needle, pinecone, root;
    public GameObject healthbar;

    private void OnEnable()
    {
        StartCoroutine("AttackSequence");
        healthbar.SetActive(true);
    }

    private void OnDisable()
    {
        healthbar.SetActive(false);
    }

    private void Update()
    {
        if (dying)
        {
            transform.localPosition = new Vector3((Mathf.Sin(Time.time * 75) * 0.2f) * development + transform.localPosition.x, transform.localPosition.y, 0); //Speed = 75, Displacement = 0.2;
            development = Mathf.Clamp01(development + 0.1f / Time.deltaTime);
            GameObject explode = Instantiate(explosion, new Vector2(Random.Range(-3, 4) + transform.position.x, Random.Range(-5, 6) + transform.position.y), Quaternion.identity);
            Destroy(explode, 2f);
            if (Time.time > decay)
            {
                GameObject death = Instantiate(explosion, transform.position, Quaternion.identity);
                death.transform.localScale = new Vector2(15, 15);
                SoundManager.PlaySound(SoundManager.Sound.blightedPineDamage);
                Destroy(death, 2f);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator AttackSequence()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Attack();
        }
    }

    public override void TakeDamage(int damage, Vector3 source)
    {
        SoundManager.PlaySound(SoundManager.Sound.blightedPineDamage);
        health -= damage;
        if (health <= 0)
        {
            dying = true;
            StopCoroutine("AttackSequence");
            decay = Time.time + 4f;
            StartCoroutine("DeathSFX");
        }

        if (floatingText)
        {
            ShowFloatingText(damage, source);
        }

        GameObject hitEffect = Instantiate(explosion2, source, Quaternion.identity);
        Destroy(hitEffect, 2f);
        GameObject explode = Instantiate(explosion, source, Quaternion.identity);
        Destroy(explode, 2f);
    }

    public override void Attack()
    {
        random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                StartCoroutine("Burst");
                break;
            case 1:
                Lob();
                break;
            case 2:
                StartCoroutine("Root");
                break;
        }
    }

    private IEnumerator Burst()
    {
        for (int i = 0; i < 3; i++)
        {
            Needles();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Needles()
    {
        for (float i = -30f; i <= 30f; i += 15f)
        {
            GameObject g = Instantiate(needle, transform.position, Quaternion.identity);
            Vector2 trajectory = (player.transform.position - transform.position).normalized;
            g.transform.eulerAngles = new Vector3(0, 0, CalculateAngle(trajectory) + i);
            SoundManager.PlaySound(SoundManager.Sound.needleShot);
        }
    }

    private void Lob()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(pinecone, transform.position, Quaternion.identity);
            SoundManager.PlaySound(SoundManager.Sound.needleShot);
        }
    }

    private IEnumerator Root()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(root, player.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            SoundManager.PlaySound(SoundManager.Sound.rootRise);
        }
    }

    private IEnumerator DeathSFX()
    {
        while (true)
        {
            SoundManager.PlaySound(SoundManager.Sound.blightedPineDamage);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private float CalculateAngle(Vector2 trajectory)
    {
        float angle = Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360;
        }

        return angle;
    }
}
