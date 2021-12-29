using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bow : ToolFunction
{
    public GameObject arrow;
    public Transform firePoint;
    private float force = 20f, development = 0;
    [Range(1, 4)]
    private int strength = 1;

    private void Start()
    {
        trigger = "Draw";
        strength = 1;
    }

    private void Update()
    {
        Rotate();

        if (strength == 4)
        {
            transform.localPosition = new Vector3((Mathf.Sin(Time.time * 75) * 0.1f) * development, -0.25f, 0); //Speed = 75, Displacement = 0.1;
            development = Mathf.Clamp01(development + 0.0001f / Time.deltaTime);
        }
        else if (strength == 1)
        {
            transform.localPosition = new Vector3(0, -0.25f, 0);
            development = 0;
        }

        if (Time.time >= cooldown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false)
                {
                    animator.SetTrigger(trigger);
                    animator.SetBool("Charging", true);
                    cooldown = Time.time + 1f / useRate;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && animator.GetBool("Charging"))
        {
            animator.SetBool("Charging", false);
        }
    }

    public override void Deal()
    {
        SoundManager.PlaySound(SoundManager.Sound.ArrowWhoosh);
        GameObject a = Instantiate(arrow, firePoint.position, gameObject.transform.rotation);
        a.GetComponent<Arrow>().speed *= strength;
        a.GetComponent<Arrow>().damage = damage * strength;
    }

    private void IncreaseStrength()
    {
        strength++;
    }

    private void ResetStrength()
    {
        strength = 1;
    }

    public void SetDamage(int setDamage)
    {
        damage = setDamage;
    }

    public int GetDamage()
    {
        return damage;
    }
}