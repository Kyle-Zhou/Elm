using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayer : MonoBehaviour
{

    public static FreezePlayer instance;
    private static bool frozen;
    private static bool hoverOverButton;
    private static bool isMovingBuilding;

    private void Start()
    {
        instance = this;
        frozen = false;
        hoverOverButton = false;
        isMovingBuilding = false;
    }

    public void SetFrozen(bool froze)
    {
        frozen = froze;
    }

    public void EnableFreeze()
    {
        frozen = true;
    }

    public void DisableFreeze()
    {
        frozen = false;
    }

    public bool GetHasBeenFrozen()
    {
        return frozen;
    }

    public void SetIsMovingBuilding(bool mb)
    {
        isMovingBuilding = mb;
    }

    public bool GetIsMovingBuilding()
    {
        return isMovingBuilding;
    }

    public void SetHoverOverButton(bool hover)
    {
        hoverOverButton = hover;
    }

    public bool GetHoverOverButton()
    {
        return hoverOverButton;
    }

}
