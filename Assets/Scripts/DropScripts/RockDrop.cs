using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDrop : Drop
{
    RockDrop()
    {
        experience = 20;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !running && BackpackManager.instance.CheckForEmptySlots())
        {
            SoundManager.PlaySound(SoundManager.Sound.ItemPickup);
            PlayerResources.instance.AddResource(1, 1);
            PlayerResources.instance.AddResourceToInventory(1, 1);

            Destroy(gameObject);
        }
    }
}
