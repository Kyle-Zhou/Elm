using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movement;
    public LayerMask dropLayer;
    public GameObject dashEffect;

    private SpriteRenderer spriterenderer;
    public bool facingRight;
    private float moveSpeed = 4.0f, previousX, increment = 0.01f, cooldown = 0;

    private void Awake()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Collider2D[] drops = Physics2D.OverlapCircleAll(transform.position, 1f, dropLayer);

        foreach (Collider2D drop in drops)
        {
            if (!drop.GetComponent<Drop>().running)
            {
                drop.transform.position += (transform.position - drop.transform.position).normalized * 0.06f;
            }
        }

        if (FreezePlayer.instance.GetHasBeenFrozen() == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", Mathf.Abs(movement.sqrMagnitude));

            if (Mathf.Abs(movement.sqrMagnitude) > 0)
            {
                //SoundManager.PlaySound(SoundManager.Sound.PlayerMove, transform.position);
                SoundManager.PlaySound(SoundManager.Sound.PlayerMove);

                if (!gameObject.transform.GetChild(9).gameObject.GetComponent<ParticleSystem>().isPlaying)
                {
                    gameObject.transform.GetChild(9).gameObject.GetComponent<ParticleSystem>().Emit(1);
                    gameObject.transform.GetChild(9).gameObject.GetComponent<ParticleSystem>().Play();
                }

                if (Input.GetKeyDown("space") && !animator.GetBool("Dashing") && Time.time >= cooldown && EnergyBar.instance.GetEnergy() > 0.05)
                {
                    animator.SetBool("Dashing", true);
                }
            }
            else
            {

                if (gameObject.transform.GetChild(9).gameObject.GetComponent<ParticleSystem>().isPlaying)
                {
                    gameObject.transform.GetChild(9).gameObject.GetComponent<ParticleSystem>().Stop();
                }
            }

            /*if (movement.x < 0 && ToolInventory.instance.GetCurrentTool() != 4)
            {
                facingRight = true;
                spriterenderer.flipX = true;
                //if (PlayerPathfinding.instance.GetPathfinding() == true) { 
                //    PlayerPathfinding.instance.CancelPathFind();
                //}
            }
            else if (movement.x > 0 && ToolInventory.instance.GetCurrentTool() != 4)
            {
                facingRight = false;
                spriterenderer.flipX = false;
                //if (PlayerPathfinding.instance.GetPathfinding() == true)
                //{
                //    PlayerPathfinding.instance.CancelPathFind();
                //}
            }*/

            if (movement.y != 0)
            {
                //if (PlayerPathfinding.instance.GetPathfinding() == true)
                //{
                //    PlayerPathfinding.instance.CancelPathFind();
                //}
            }
        }
    }

    void FixedUpdate()
    {
        if (FreezePlayer.instance.GetHasBeenFrozen() == false || animator.GetBool("Dashing")) 
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void Dash()
    {

        //ADD CONDITION TO CHECK IF ENERGY IS OVER 0.1;

        //GameObject de = Instantiate(dashEffect, transform.position, Quaternion.identity);
        //Destroy(de, 2f);

        SoundManager.PlaySound(SoundManager.Sound.playerDash);

        EnergyBar.instance.GainEnergy(-0.05f);
        EnergyBar.instance.SetPreviousDash(Time.time);
        cooldown = Time.time + 0.25f;
        StartCoroutine("PlaceAfterimages");
        FreezePlayer.instance.SetFrozen(true);
        moveSpeed = 12f;
    }

    public void EndDash()
    {
        animator.SetBool("Dashing", false);
        StopCoroutine("PlaceAfterimages");
        FreezePlayer.instance.SetFrozen(false);
        moveSpeed = 4f;
    }

    private IEnumerator PlaceAfterimages()
    {
        AfterimagePool.instance.LayAfterimage();
        previousX = transform.position.x;
        while (true)
        {
            yield return new WaitForSeconds(increment);
            AfterimagePool.instance.LayAfterimage();
            previousX = transform.position.x;
        }
    }

    public float getMovementX()
    {
        return movement.x;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float setMoveSpeed)
    {
        moveSpeed = setMoveSpeed;
    }
}