using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolFunction : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sr, psr;
    public LayerMask interactionLayer;
    public PlayerMovement pm;
    public Transform hitboxCentre;
    public CircleCollider2D hitbox;

    public string trigger;
    public float useRate, cooldown = 0, useRange;
    public int damage;

    private void Update()
    {
        Rotate();

        if (Time.time >= cooldown)
        {
            if (Input.GetMouseButton(0))
            {
                if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false)
                {

                    animator.SetTrigger(trigger);
                    SoundManager.PlaySound(SoundManager.Sound.playerAttack);

                    cooldown = Time.time + 1f / useRate;
                }
            }
        }
    }

    public float CalculateAngle(Vector2 trajectory)
    {
        float angle = Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360;
        }

        return angle;
    }

    public void Rotate()
    {
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(cursor.x - transform.position.x, cursor.y - transform.position.y);

        if (cursor.x < transform.position.x)
        {
            sr.flipY = true;
            psr.flipX = true;
            pm.facingRight = true;
        }
        else if (cursor.x >= transform.position.x)
        {
            sr.flipY = false;
            psr.flipX = false;
            pm.facingRight = false;
        }

        transform.eulerAngles = new Vector3(0, 0, CalculateAngle(direction));
    }

    public abstract void Deal();

    void OnDrawGizmosSelected()
    {
        if (hitboxCentre == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(hitboxCentre.position, useRange);
    }
}
