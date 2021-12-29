using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStates : RenewableStates
{
    public List<Sprite> spritePool;
    private int rand;

    private void Start()
    {
        maxHealth = 4;
        rand = Random.Range(0, 2);
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spritePool[rand];
        passive = spritePool[rand];
        selected = spritePool[rand + 2];
    }

    private void OnEnable()
    {
        gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
    }

    public override void TakeHit(int damage)
    {
        if (mature)
        {
            gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Emit(Random.Range(3, 7));

            SoundManager.PlaySound(SoundManager.Sound.TreeHit);

            health -= damage;
            bar.GetComponent<SpriteRenderer>().enabled = true;

            if (health >= 0)
            {
                bar.GetComponent<SpriteRenderer>().sprite = bars[health];
            }
            else
            {
                bar.GetComponent<SpriteRenderer>().sprite = bars[0];
            }

            ScreenShake.instance.Shake(25, 3);

            if (health <= 0) //tree death
            {
                mature = false;
                upperCollider.enabled = false;
                sr.sprite = leftover;
                stumpShadow.SetActive(true);
                treeShadow.SetActive(false);

                timeOfDeath = Time.time;
                for (int i = 0; i < 3; i++)
                {
                    float x = Random.Range(gameObject.transform.position.x + 0.3f, gameObject.transform.position.x - 0.3f);
                    float y = Random.Range(gameObject.transform.position.y + 0.3f, gameObject.transform.position.y - 0.3f);
                    Vector3 spawn = new Vector2(x, y);
                    if (Random.Range(1, 6) == 5)
                    {
                        //Instantiate(drops[1], spawn, transform.rotation);
                    }
                    Instantiate(drops[0], spawn, transform.rotation);
                }
            }
        }

    }

    public override void MachineHit(int damage)
    {
        if (mature)
        {
            health -= damage;
            bar.GetComponent<SpriteRenderer>().enabled = true;
            bar.GetComponent<SpriteRenderer>().sprite = bars[health];

            if (health <= 0) //tree death
            {
                bar.GetComponent<SpriteRenderer>().enabled = false;
                mature = false;
                upperCollider.enabled = false;
                sr.sprite = leftover;
                timeOfDeath = Time.time;
                //for (int i = 0; i < 3; i++)
                //{
                //                               //probably change this later
                //    float x = Random.Range(gameObject.transform.position.x + 0.3f, gameObject.transform.position.x - 0.3f);
                //    float y = Random.Range(gameObject.transform.position.y + 0.3f, gameObject.transform.position.y - 0.3f);
                //    Vector3 spawn = new Vector2(x, y);
                //    Instantiate(drops[0], spawn, transform.rotation);
                //}
            }
        }
    }

}
