using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineconeBomb : MonoBehaviour
{
    public GameObject source;
    public GameObject fragment;
    public Collider2D hitbox;
    private float direction;
    private Vector2 endpoint, end, start;
    private bool running;

    private void Start()
    {
        direction = Random.value;
        start = transform.position;
        end = CalculateEnd();
        StartCoroutine(Arch());
        hitbox.enabled = false;
    }

    private void Update()
    {
        if (!running)
        {
            SoundManager.PlaySound(SoundManager.Sound.pineconeShatter);
            hitbox.enabled = true;
            int angle = 0;
            for (int i = 0; i < 8; i++)
            {
                GameObject g = Instantiate(fragment, transform.position, Quaternion.identity);
                g.transform.eulerAngles = new Vector3(0, 0, angle);
                angle += 45;
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBar.instance.SubtractHealth(source.GetComponent<BlightedPine>().damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("WallTilemap"))
        {
            Destroy(gameObject);
        }
    }

    private Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        System.Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }

    private Vector2 CalculateEnd()
    {
        if (direction >= 0.5)
        {
            endpoint = new Vector2(transform.position.x + Random.Range(5f, 10f), transform.position.y - Random.Range(-5f, 10f));
        }
        else
        {
            endpoint = new Vector2(transform.position.x - Random.Range(5f, 10f), transform.position.y - Random.Range(-5f, 10f));
        }
        return endpoint;
    }

    IEnumerator Arch()
    {
        running = true;
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            transform.position = Parabola(start, end, 7f, i);
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 10f);
            yield return null;
        }
        running = false;
    }
}
