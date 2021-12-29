using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingStates : ResourceStates
{

    private float startTime;
    private float growTime = 30.0f;

    public GameObject treePrefab;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + growTime)
        {
            Instantiate(treePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
