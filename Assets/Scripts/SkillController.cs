using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public static SkillController instance;
    public Animator characterAnimator, swordAnimator;
    public CircleCollider2D hitbox;
    public LayerMask interactionLayer;

    private int skillPoints;
    private float cooldown;
    private bool skillOneUnlocked = false, skillTwoUnlocked = false; //rename 

    public void Start()
    {
        instance = this;
    }

    public void Update()
    {
        //skill one
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //LevelBar.instance.AddXP(50);
            if (skillOneUnlocked)
            {
                if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false
                    && ToolInventory.instance.GetCurrentTool() == 3 && Time.time >= cooldown)
                {
                    characterAnimator.SetTrigger("Heavy Attack");
                    swordAnimator.SetTrigger("Heavy Attack");
                    cooldown = Time.time + 5f;
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.T)) //TEMPORARY
        //{
        //    LevelBar.instance.AddXP(50);
        //}

        //if (Input.GetKeyDown(KeyCode.R)){
        //    if (skillOneUnlocked)
        //    {
        //        if (FreezePlayer.instance.GetHasBeenFrozen() == false && FreezePlayer.instance.GetHoverOverButton() == false
        //            && ToolInventory.instance.GetCurrentTool() == 4 && Time.time >= cooldown)
        //        {
        //            characterAnimator.SetTrigger("Heavy Attack");
        //            swordAnimator.SetTrigger("Heavy Attack");
        //            cooldown = Time.time + 5f;
        //        }
        //    }
        //}

    }

    public int GetSkillPoints()
    {
        return skillPoints;
    }

    public void AddSkillPoints(int addAmount)
    {
        skillPoints += addAmount;
    }

    public void UnlockSkillOne()
    {
        skillOneUnlocked = true;
    }

    public void DealHeavyAttack()
    {
        SoundManager.PlaySound(SoundManager.Sound.heavyAttack);
        Collider2D[] targets = Physics2D.OverlapCircleAll(hitbox.transform.position, hitbox.radius, interactionLayer);

        foreach (Collider2D target in targets)
        {
            if (!target.GetComponent<Mob>().invulnerable && !target.GetComponent<Mob>().dying)
            {
                target.GetComponent<Mob>().TakeDamage(100, hitbox.transform.position);
            }
        }
    }

    public void UnlockSkillTwo()
    {
        skillTwoUnlocked = true;
    }

}
