using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockStates : MineralStates
{
    public List<Sprite> spritePool;
    private int rand;

    public GameObject flatShadow;
    public GameObject spikeShadow;

    private void Start()
    {
        health = 3;
        rand = Random.Range(0, 2);
        sr.sprite = spritePool[rand];
        passive = spritePool[rand];

        if (rand == 0)
        {
            flatShadow.SetActive(true);
            spikeShadow.SetActive(false);
        } else if (rand == 1)
        {
            flatShadow.SetActive(false);
            spikeShadow.SetActive(true);
        }

        selected = spritePool[rand + 2];
    }

    private void OnEnable()
    {
        gameObject.transform.GetChild(2).GetComponent<ParticleSystem>().Stop();
    }

    public override void TakeHit(int damage)
    {
        gameObject.transform.GetChild(2).GetComponent<ParticleSystem>().Emit(Random.Range(3, 7));
        health -= damage;
        SoundManager.PlaySound(SoundManager.Sound.RockHit);

        ScreenShake.instance.Shake(25, 3);

        if (health <= 0) //rock death
        {
            for (int i = 0; i < 3; i++)
            {
                float x = Random.Range(gameObject.transform.position.x + 0.3f, gameObject.transform.position.x - 0.3f);
                float y = Random.Range(gameObject.transform.position.y + 0.3f, gameObject.transform.position.y - 0.3f);
                Vector3 spawn = new Vector2(x, y);
                Instantiate(drops[0], spawn, transform.rotation);
            }
            spawner.AdjustRocks(-1);
            Destroy(gameObject);
        }
    }

    public override void MachineHit(int damage)
    {

    }
}
