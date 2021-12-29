using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDrop : Drop
{
    SlimeDrop()
    {
        experience = 10;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !running && BackpackManager.instance.CheckForEmptySlots())
        {
            LevelBar.instance.AddXP(experience);
            SoundManager.PlaySound(SoundManager.Sound.ItemPickup);
            PlayerResources.instance.AddResource(1, 15);
            PlayerResources.instance.AddResourceToInventory(1, 15);
            Destroy(gameObject);
        }
    }
}
