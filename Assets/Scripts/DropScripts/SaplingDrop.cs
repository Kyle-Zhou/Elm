using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingDrop : Drop
{
    SaplingDrop()
    {
        experience = 20;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !running && BackpackManager.instance.CheckForEmptySlots())
        {
            SoundManager.PlaySound(SoundManager.Sound.ItemPickup);
            PlayerResources.instance.AddResource(1, 10);
            PlayerResources.instance.AddResourceToInventory(1, 10);

            Destroy(gameObject);
        }
    }
}
