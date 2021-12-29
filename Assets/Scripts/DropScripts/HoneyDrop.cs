using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyDrop : Drop
{
    HoneyDrop()
    {
        experience = 10;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !running && BackpackManager.instance.CheckForEmptySlots())
        {
            SoundManager.PlaySound(SoundManager.Sound.ItemPickup);
            PlayerResources.instance.AddResource(1, 16);
            PlayerResources.instance.AddResourceToInventory(1, 16);
            LevelBar.instance.AddXP(experience);
            Destroy(gameObject);         
        }
    }
}
