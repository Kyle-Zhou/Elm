using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOnEnable : MonoBehaviour
{


    private void OnEnable()
    {
        Menu.instance.SetAnyMenuOpen(true);
    }

    private void OnDisable()
    {
        Menu.instance.SetAnyMenuOpen(false);
    }
}
