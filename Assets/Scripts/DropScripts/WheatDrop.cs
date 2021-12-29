using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatDrop : Drop
{
    WheatDrop()
    {
        experience = 20;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !running && BackpackManager.instance.CheckForEmptySlots())
        {
            SoundManager.PlaySound(SoundManager.Sound.ItemPickup);
            PlayerResources.instance.AddResource(1, 14);
            PlayerResources.instance.AddResourceToInventory(1, 14);
            Destroy(gameObject);
        }
    }
}
