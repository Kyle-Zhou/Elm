using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomInitialization : MonoBehaviour
{
    public void Update()
    {
        SetParentToRoom();
        enabled = false;
    }

    public void SetParentToRoom()
    {
        gameObject.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);
    }
}
