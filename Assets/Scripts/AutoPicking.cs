using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPicking : Machine
{

    AutoPicking()
    {
        collectionTime = true;
        timerWaitTime = 2.0f;
    }


    public GameObject reachingBounds;
    private float attackRange;
    private GameObject buildingMenu;

    //public float incrementRate = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        attackRange = 0.5f;
    }


    public override IEnumerator Timer()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2.0f);
        }

        Collider2D[] hitObstacles = Physics2D.OverlapCircleAll(reachingBounds.transform.position, attackRange);
        //bool oneTreeOnly = true; //only allows for one hitbox of the tree to be hit

        foreach (Collider2D obstacle in hitObstacles)
        {
            if (obstacle.CompareTag("BlueberryBush"))
            {
                //obstacle.GetComponent<TreeCollision>().TakeHitFromMachine(1);
                obstacle.GetComponent<BlueberryBushStates>().MachineCollect();
                //buildingMenu.GetComponent<CollectionSystem>().AddBlueberries(1);
                buildingMenu.GetComponent<CollectionSystem>().AddResource(1);
            }
        }
        collectionTime = true;
    }

  
    public override void SetBuildingMenu(GameObject wsm)
    {
        buildingMenu = wsm;
    }

    //------------- THIS IS TO VIEW THE OVERLAPCIRCLE ------------
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(reachingBounds.transform.position, attackRange);
    }

}
