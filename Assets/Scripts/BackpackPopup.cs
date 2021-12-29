using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackPopup : MonoBehaviour
{

    public static BackpackPopup instance;
    //public GameObject[] items;
    public GameObject[] popUpMenus;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
    }

    public void SwitchPopUp(int index)
    {
        popUpMenus[index].SetActive(true);
    }


    public void RemovePopUp(int index)
    {
        popUpMenus[index].SetActive(false);
    }
}


