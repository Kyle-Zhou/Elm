using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public float visionRadius, baseRadius, alertedRadius;
    [Range(0, 360)]
    public float visionAngle, baseAngle;
    public LayerMask interactionLayer;
    public bool detected, check;
    private Vector3 facing;

    private void OnEnable()
    {
        StartCoroutine(Look());
    }

    private IEnumerator Look()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (!this.GetComponent<Mob>().dying) {
                LocateTargets();
            }
        }
    }

    private void LocateTargets()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, visionRadius, interactionLayer);

        foreach (Collider2D target in targets)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;

            if (this.GetComponent<Mob>().rightward)
            {
                facing = transform.right;
            }
            else
            {
                facing = -transform.right;
            }

            if (Vector3.Angle(facing, direction) < visionAngle / 2 || detected)
            {
                detected = true;

                if (target.transform.position.x > this.transform.position.x)
                {
                    this.GetComponent<Mob>().rightward = true;
                    this.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    this.GetComponent<Mob>().rightward = false;
                    this.GetComponent<SpriteRenderer>().flipX = true;
                }

                if (!this.GetComponent<Mob>().stunned && !this.GetComponent<Mob>().attacking && !this.GetComponent<Mob>().inRange)
                {
                    this.GetComponent<Mob>().SetMovement(target.transform.position);
                }

                if (check != detected && !check)
                {
                    this.GetComponent<Mob>().Stretch();
                    this.GetComponent<Mob>().StopCoroutine("Wander");
                    gameObject.transform.GetChild(0).GetComponent<NotificationFade>().StartCoroutine("Fade");
                    visionRadius = alertedRadius;
                    visionAngle = 360f;
                    check = detected;
                }
            }
        }

        if (check != detected && check)
        {
            this.GetComponent<Mob>().ResetMovement();
            this.GetComponent<Mob>().StartCoroutine("Wander");
            gameObject.transform.GetChild(1).GetComponent<NotificationFade>().StartCoroutine("Fade");
            visionRadius = baseRadius;
            visionAngle = baseAngle;
            check = detected;
        }

        detected = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.GetComponent<Mob>().rightward && collision.transform.position.x < this.transform.position.x)
            {
                this.GetComponent<Mob>().rightward = false;
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (!this.GetComponent<Mob>().rightward && collision.transform.position.x > this.transform.position.x)
            {
                this.GetComponent<Mob>().rightward = true;
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    public Vector3 AngleDirection(float angle, bool global)
    {
        if (global)
        {
            angle -= transform.eulerAngles.z;
        }

        if (this.GetComponent<Mob>().rightward)
        {
            return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        }
        else
        {
            return new Vector3(-Mathf.Cos(angle * Mathf.Deg2Rad), -Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        }
    }
}