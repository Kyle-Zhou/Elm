using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    public bool collectionTime;
    public float timerWaitTime;
    public Animator animator;
    public SpriteRenderer sr;
    public Sprite noEnergyVersion;

    // Update is called once per frame
    private void Update()
    {
        if (collectionTime)
        {
            if (PowerGeneratorEnergy.instance.GetEnergized() == true)
            {
                animator.enabled = true;
                if (FreezePlayer.instance.GetIsMovingBuilding() == false)
                {
                    collectionTime = false;
                    StartCoroutine(Timer());
                }
            }
            else
            {
                animator.enabled = false;
                sr.sprite = noEnergyVersion;
            }
        }
    }

    public abstract void SetBuildingMenu(GameObject wsm);

    public abstract IEnumerator Timer();


}
