using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemManager : MonoBehaviour
{
    public static TotemManager instance;
    private int currentTotemLevel = 1;
    private int currentBossLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;   
    }

    public int GetCurrentTotemLevel()
    {
        return currentTotemLevel;
    }

    public void UpgradeCurrentTotemLevel()
    {
        currentTotemLevel += 1;
    }

    public int GetCurrentBossLevel()
    {
        return currentBossLevel;
    }
    public void UpgradeCurrentBossLevel()
    {
        currentBossLevel += 1;
    }

}
