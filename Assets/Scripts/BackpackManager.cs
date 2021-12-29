using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{

    public static BackpackManager instance;

    public GameObject[] slots;
    //public GameObject[] itemButtons;

    private void Start()
    {
        instance = this;    
    }

    public void SetSlot(GameObject button)
    {
        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (slots[i].transform.childCount <= 0)
            {
                button.transform.SetParent(slots[i].transform);
                button.transform.position = slots[i].transform.position;
            }

        }
    }

    public void RemoveSlot(GameObject button)
    {
        button.transform.SetParent(null);
    }


    public int DroppedOnSlot(GameObject slot)
    {
        if (slot.transform.childCount <= 0)
        {
            for (int i = slots.Length - 1; i >= 0; i--)
            {
                if (slots[i].name == slot.name)
                {
                    return i;
                }

            }
        }
        if (slot.transform.childCount >= 1)
        {
            for (int i = slots.Length - 1; i >= 0; i--)
            {
                if (slots[i].name == slot.name)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public bool CheckForEmptySlots()
    {
        bool empty = false;
        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (slots[i].transform.childCount <= 0)
            {
                empty = true;
            }
        }
        return empty;
    }


}