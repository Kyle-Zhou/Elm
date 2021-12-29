using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : ToolFunction
{
    public GameObject critEffect, slash;
    private int clicks, multiplier;
    private float previousClick, delay = 1f;
    private bool swing, forth = true;

    private void Start()
    {
        useRate = 2f;
        hitbox.radius = useRange;
        damage = 25;
        multiplier = 1;
        trigger = "Basic Attack";
    }

    private void Update()
    {
        Rotate();

        if (forth && !swing)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (!forth && !swing)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }

        if (Time.time - previousClick > delay)
        {
            clicks = 0;
            multiplier = 1; //Just in case?
        }

        if (Time.time >= cooldown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false)
                {
                    previousClick = Time.time;
                    clicks++;
                    clicks = Mathf.Clamp(clicks, 0, 3);

                    SoundManager.PlaySound(SoundManager.Sound.playerAttack);

                    switch (clicks)
                    {
                        case 1:
                            multiplier = 1;
                            animator.SetTrigger(trigger);
                            cooldown = Time.time + 1f / useRate;
                            break;
                        case 2:
                            animator.SetTrigger(trigger);
                            cooldown = Time.time + 1f / useRate;
                            break;
                        case 3:
                            clicks = 0;
                            multiplier = 2;
                            animator.SetTrigger(trigger);
                            cooldown = Time.time + 1f / useRate;
                            break;
                    }
                }
            }
        }
    }

    public new void Rotate()
    {
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(cursor.x - transform.position.x, cursor.y - transform.position.y);

        if (cursor.x < transform.position.x)
        {
            sr.flipY = true;
            psr.flipX = true;
            pm.facingRight = true;

            if (!swing)
            {
                if (forth)
                {
                    transform.eulerAngles = new Vector3(0, 0, CalculateAngle(direction) - 30);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, CalculateAngle(direction) + 30);
                }
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, CalculateAngle(direction));
            }
        }
        else if (cursor.x >= transform.position.x)
        {
            sr.flipY = false;
            psr.flipX = false;
            pm.facingRight = false;

            if (!swing)
            {
                if (forth)
                {
                    transform.eulerAngles = new Vector3(0, 0, CalculateAngle(direction) + 30);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, CalculateAngle(direction) - 30);
                }
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, CalculateAngle(direction));
            }
        }
    }

    public override void Deal()
    {
        swing = true;
        GameObject g = Instantiate(slash, transform.position, Quaternion.identity);
        g.GetComponent<Slash>().damage = damage * multiplier;

        if (pm.facingRight)
        {
            if (forth)
            {
                g.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 30);
            }
            else
            {
                g.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 30);
            }
        }
        else
        {
            if (forth)
            {
                g.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 30);
            }
            else
            {
                g.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 30);
            }
        }

        forth = !forth;
        swing = false;

        Collider2D[] vegTargets = Physics2D.OverlapCircleAll(gameObject.transform.position, hitbox.radius, interactionLayer);

        foreach (Collider2D target in vegTargets)
        {
            if (target.CompareTag("Vegetable"))
            {
                target.gameObject.GetComponent<VegetableStates>().TakeHit(1);
            }
        }

    }

    private void ClearSwing()
    {
        swing = false;
    }

    public void SetDamage(int setDamage)
    {
        damage = setDamage;
    }
}
