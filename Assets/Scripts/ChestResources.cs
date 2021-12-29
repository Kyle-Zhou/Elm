using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestResources : MonoBehaviour
{

    //NO PUBLIC STATIC INSTANCE
    //

    private int[] quantities = new int[18];

    public void Start()
    {
    }

    public void AddResource(int value, int index)
    {
        this.quantities[index] += value;
    }

    public int GetWood()
    {
        return quantities[0];
    }

    public int GetRocks()
    {
        return quantities[1];
    }

}
