using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitionAnims : MonoBehaviour
{
    public static RoomTransitionAnims instance; 
    public Animator transitionAnimNorth;
    public Animator transitionAnimSouth;
    public Animator transitionAnimEast;
    public Animator transitionAnimWest;

    public void Start()
    {
        instance = this;
    }

    public void MovingUpAnimStart()
    {
        transitionAnimNorth.SetTrigger("StartNorth");
    }

    public void MovingUpAnimEnd()
    {
        transitionAnimNorth.SetTrigger("EndNorth");
    }

    public void MovingDownAnimStart()
    {
        transitionAnimSouth.SetTrigger("StartSouth");
    }

    public void MovingDownAnimEnd()
    {
        transitionAnimSouth.SetTrigger("EndSouth");
    }


    public void MovingRightAnimStart()
    {
        transitionAnimEast.SetTrigger("StartEast");
    }
    public void MovingRightAnimEnd()
    {
        transitionAnimEast.SetTrigger("EndEast");
    }


    public void MovingLeftAnimStart()
    {
        transitionAnimWest.SetTrigger("StartWest");
    }
    public void MovingLeftAnimEnd()
    {
        transitionAnimWest.SetTrigger("EndWest");
    }


}
