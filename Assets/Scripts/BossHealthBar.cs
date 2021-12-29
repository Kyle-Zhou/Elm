using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public BlightedPine boss;
    public Image mask;

    void Update()
    {
        float fillAmount = (float)boss.health / (float)boss.maxHealth;
        mask.fillAmount = fillAmount;
    }
}
