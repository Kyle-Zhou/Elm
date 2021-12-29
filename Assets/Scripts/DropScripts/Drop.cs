using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Drop : MonoBehaviour
{
    private float direction;
    private Vector2 endpoint, end, start;
    public bool running;
    public int experience;
    private float startTime;
    private float lifeTime = 25f;

    private void Start()
    {

        gameObject.transform.SetParent(RoomManager.instance.GetCurrentRoomInstance().transform);
        startTime = Time.time;

        direction = Random.value;
        start = transform.position;
        end = CalculateEnd();
        StartCoroutine(Arch());
    }

    public void Update()
    {
        if (Time.time > startTime + lifeTime)
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
            endpoint = new Vector2(transform.position.x + Random.Range(0.5f, 0.8f), transform.position.y);
        }
        else
        {
            endpoint = new Vector2(transform.position.x - Random.Range(0.5f, 0.8f), transform.position.y);
        }
        return endpoint;
    }

    IEnumerator Arch()
    {
        running = true;
        for (float i = 0; i <= 1; i += Time.deltaTime * 2)
        {
            transform.position = Parabola(start, end, 0.5f, i);
            yield return null;
        }
        running = false;
    }
}
