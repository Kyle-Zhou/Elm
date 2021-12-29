using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoChopping : Machine
{

    AutoChopping()
    {
        collectionTime = true;
        timerWaitTime = 0.5f;
    }

    private GameObject buildingMenu;
    private float attackRange;

    private void Start()
    {
        attackRange = 1.0f;
    }


    public override IEnumerator Timer()
    {
        for (int i = 0; i < 5; i++) { 
            yield return new WaitForSeconds(timerWaitTime);
        }
        Collider2D[] hitObstacles = Physics2D.OverlapCircleAll(gameObject.transform.position, attackRange);
        foreach (Collider2D obstacle in hitObstacles)
        {
            if (obstacle.CompareTag("NormalTree"))
            {
                obstacle.GetComponent<TreeStates>().MachineHit(1);
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
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
    }

}
