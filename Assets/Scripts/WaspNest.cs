using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspNest : MonoBehaviour
{
    public Mob wasp;
    private float startTime;
    private float increment = 15f;
    private int spawnsCount;

    void Start()
    {
        startTime = Time.time;

		StartSpawnWasps(4);
	}

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + increment) {
            startTime = Time.time;
            SpawnNewWasps(2);
            spawnsCount++;
            if (spawnsCount > 4)
            {
                enabled = false;
            }
        }
    }

    public void StartSpawnWasps(int n)
    {
        for (int i = 0; i < n; i++)
        {
            int[] options = {-3, 2, 1, 0, -1, -2, -3}; //tentative -> this is used because im not sure if i want to use floats or not
                                                       //but if not can just change it to randomDifference1 = Random.Range(-3,3);
            int randomDifference1 = options[Random.Range(0, 6)];
            int randomDifference2 = options[Random.Range(0, 6)];

            float randomX = gameObject.transform.position.x + randomDifference1;
            float randomY = gameObject.transform.position.y + randomDifference2;

            Vector2 spawnpoint = new Vector2(randomX, randomY);
            Mob m = Instantiate(wasp, spawnpoint, Quaternion.identity);
            m.transform.SetParent(gameObject.transform);
        }
    }

    public void SpawnNewWasps(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Vector2 spawnpoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Mob m = Instantiate(wasp, spawnpoint, Quaternion.identity);
            m.transform.SetParent(gameObject.transform);


            //this works? not as intended but it's worse when removed

            //int[] options = { 3, 2, 1, 0, -1, -2, -3 }; //tentative (see above)
            int[] options = { 3, 4, 5, 6, 7, 8, 9 }; //tentative (see above)

            int randomDifference1 = options[Random.Range(0, 6)];
            int randomDifference2 = options[Random.Range(0, 6)];

            float randomX = gameObject.transform.position.x + randomDifference1;
            float randomY = gameObject.transform.position.y + randomDifference2;

            Vector2 newPosition = new Vector2(randomX, randomY);

            //distance formula
            float xSubtractSquared = (gameObject.transform.position.x - randomX) * (gameObject.transform.position.x - randomX);
            float ySubtractSquared = (gameObject.transform.position.y - randomY) * (gameObject.transform.position.y - randomY);
            float distance = Mathf.Sqrt(xSubtractSquared + ySubtractSquared);

            m.transform.position = Vector3.MoveTowards(gameObject.transform.position, newPosition, Time.deltaTime * 1f);


            //speed / distance
        }
    }



}
