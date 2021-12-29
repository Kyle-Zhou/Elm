using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldMining : Machine
{

    GoldMining()
    {
        collectionTime = true;
        timerWaitTime = 0.5f;
    }

    private GameObject buildingMenu;

    public override IEnumerator Timer()
    {
        for (int i = 0; i < 10; i++) //make this longer
        {
            yield return new WaitForSeconds(1.0f);

        }

        //play animation of cart coming up
        buildingMenu.GetComponent<CollectionSystem>().AddResource(1);

        collectionTime = true;

    }


    public override void SetBuildingMenu(GameObject wsm)
    {
        buildingMenu = wsm;
    }

}
