using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsIcon : MonoBehaviour
{
    private bool unlocked = false;
    private Image img;
    public Animator animator;
    private bool available = false;
    public RectTransform rt;

    public void Awake()
    {
        img = GetComponent<Image>();
    }


    public void OnEnable()
    {
        if (!unlocked && available)
        {
            if (SkillController.instance.GetSkillPoints() >= 1)
            {
                animator.enabled = true;
            }
        }
    }

    public bool GetUnlocked()
    {
        return unlocked;
    }

    public void Check()
    {
        if (unlocked == false)
        {
            if (available && SkillController.instance.GetSkillPoints() >= 1)
            {
                animator.enabled = true;
            } else
            {
                animator.enabled = false;
                rt.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void Unlock()
    {
        unlocked = true;
        animator.enabled = false;
        rt.localScale = new Vector3(1,1,1);
        img.color = Color.green;
    }

    public bool GetAvailable()
    {
        return available;
    }
    public void SetAvailable(bool a)
    {
        available = a;
    }

}
