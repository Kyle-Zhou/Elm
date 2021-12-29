using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotStates : VegetableStates
{

    private void Start()
    {
        health = 1;
    }

    public override void TakeHit(int damage)
    {
        SoundManager.PlaySound(SoundManager.Sound.CollectWheatAndCarrots);
        health -= damage;
        //ScreenShake.instance.StartShake(.2f, .1f);

        if (health <= 0) //could just get rid of this
        {
            for (int i = 0; i < 3; i++)
            {
                float x = Random.Range(gameObject.transform.position.x + 0.3f, gameObject.transform.position.x - 0.3f);
                float y = Random.Range(gameObject.transform.position.y + 0.3f, gameObject.transform.position.y - 0.3f);
                Vector3 spawn = new Vector2(x, y);
                Instantiate(drops[0], spawn, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    public override void MachineHit(int damage)
    {

    }
}
