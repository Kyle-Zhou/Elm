using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolUpgrades : MonoBehaviour
{

    public static ToolUpgrades instance;
    private int [] levels = {1,1,1,1,1};

    public List<GameObject> tools;


    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
    }

    public void UpgradeTools(int index)
    {
        levels[index]++;

        switch (index) {
            case 0: //axe
                tools[0].GetComponent<Chopping>().SetDamage(levels[index] * 2);
                //switch sprite
                break;
            case 1: //pickaxe
                tools[1].GetComponent<Mining>().SetDamage(levels[index] * 2);
                break;
            case 2: //shovel???
                break;
            case 3: //sword
                tools[3].GetComponent<BasicAttack>().SetDamage(30);
                break;
        }
    }

    public int GetToolLevel(int toolIndex)
    {
        return levels[toolIndex];
    }
    //public int GetSwordIndex()
    //{
    //    return 3;
    //}


    //public int GetLevel()
    //{
    //    return level;
    //}

}
