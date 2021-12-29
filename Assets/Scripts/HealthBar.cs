using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    public Animator animator;
    public Image mask;
    public GameObject gameOverScreen;
    private int maxHealth = 100;
    private static int health;

    private void Start()
    {
        instance = this;
        health = 100;
        //foreach (Transform child in transform)
        //{
        //    hearts[index] = child.GetChild(0).GetComponent<UIHeart>();
        //    index++;
        //}
    }

    private void Update()
    {
        /*if (Input.GetKeyDown("space"))
        {
            SubtractHealth(100);
        }*/
    }

    public int GetHealth()
    {
        return health;
    }

    public void SubtractHealth(int subtractAmount)
    {
        if (health > 0) {
            ScreenShake.instance.Shake(25, 1);
            animator.SetTrigger("Damage");
            health -= subtractAmount;

            if (health <= 0)
            {
                SoundManager.PlaySound(SoundManager.Sound.playerDeath);
                animator.SetBool("Dead", true);
                gameOverScreen.SetActive(true);
                StartCoroutine(ExecuteAfterTime());
            }
            else
            {
                SoundManager.PlaySound(SoundManager.Sound.playerDamage);
            }

        }

        float fillAmount = (float)health / (float)maxHealth;
        mask.fillAmount = fillAmount;
    }

    IEnumerator ExecuteAfterTime() //lazy oops
    {
        yield return new WaitForSeconds(0.5f);
        AudioListener.volume = 0.8f;
        yield return new WaitForSeconds(0.5f);
        AudioListener.volume = 0.6f;
        yield return new WaitForSeconds(0.5f);
        AudioListener.volume = 0.4f;
        yield return new WaitForSeconds(0.5f);
        AudioListener.volume = 0.2f;
        yield return new WaitForSeconds(0.5f);
        AudioListener.volume = 0f;
    }


    public void AddHealth(int addAmount)
    {
        if (health < 100)
        {
            if (addAmount > (maxHealth - health))
            {
                addAmount = maxHealth - health;
            }

            health += addAmount;
        }

        float fillAmount = (float)health / (float)maxHealth;
        mask.fillAmount = fillAmount;
    }

    public void SetHealth(int setAmount)
    {
        health = setAmount;
    }

}
