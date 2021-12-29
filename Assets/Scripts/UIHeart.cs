using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour
{
    public Image heart;
    public Sprite[] states;

    private int heartHealth;
    private int maxHeartHealth = 20;

    private void Start()
    {
        heartHealth = maxHeartHealth;
    }

    private void Update()
    {
        if (heartHealth == maxHeartHealth)
        {
            heart.sprite = states[0];
        }
        else if (heartHealth >= 15 && heartHealth < maxHeartHealth)
        {
            heart.sprite = states[1];
        }
        else if (heartHealth >= 8 && heartHealth < 15)
        {
            heart.sprite = states[2];

        }
        else if (heartHealth >= 1 && heartHealth < 8)
        {
            heart.sprite = states[3];
        }
        else
        {
            heart.sprite = null;
            heart.enabled = false;
        }
    }

    public void SetHearthHealth(int addAmount)
    {
        heartHealth += addAmount;
    }

    public int GetHeartHealth()
    {
        return heartHealth;
    }
}
