using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gatekeeper : MonoBehaviour
{
    public int maxHealth, health, damage;
    public float attackRate, cooldown, development = 0;
    public bool dying;

    public SpriteRenderer sr;
    public Animator animator;
    public LayerMask interactionLayer;
    public GameObject deathExplosion, explosion, explosion2;
    public GameObject floatingText;

    public abstract void TakeDamage(int damage, Vector3 source);

    public abstract void Attack();

    public void ShowFloatingText(int damage, Vector3 source)
    {
        var x = Instantiate(floatingText, source, Quaternion.identity, transform);
        x.GetComponent<TextMesh>().text = damage.ToString();
    }
}
