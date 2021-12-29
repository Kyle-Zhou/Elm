using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroBox : MonoBehaviour
{

    void Update()
    {
        FreezePlayer.instance.SetFrozen(true);
        if (Input.GetMouseButton(0))
        {
            SoundManager.PlaySound(SoundManager.Sound.clickToPop);
            FreezePlayer.instance.SetFrozen(false);
            enabled = false;
            Destroy(gameObject);
        }
    }

 
}
