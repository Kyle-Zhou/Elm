using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotDrop : Drop
{
    CarrotDrop()
    {
        experience = 15;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !running && BackpackManager.instance.CheckForEmptySlots())
        {
            SoundManager.PlaySound(SoundManager.Sound.ItemPickup);
            PlayerResources.instance.AddResource(1, 12);
            PlayerResources.instance.AddResourceToInventory(1, 12);
            Destroy(gameObject);
        }
    }
}