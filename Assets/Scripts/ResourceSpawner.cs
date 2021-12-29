using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public List<GameObject> resources;
    private static int[] quantities = new int[2];
    private List<Vector3> existingTiles;
    private int tileIndex;
    public float obstacleCheckRadius = 1f;

    private void Start()
    {
        quantities[0] = 3; //temp
    }

    private void Update()
    {
        if (GetRocks() <= 1) { 
            //if (Time.time >= startTime + rockSpawnInterval)
            //{
            //    AddRock();
            //    //Debug.Log("rock function called");
            //    startTime = Time.time;
            //}
            StartCoroutine(SpawnRock());
        }
    }

    public IEnumerator SpawnRock()
    {
        yield return new WaitForSeconds(3f);
        //AddRock();
    }

    public void AddRock()
    {
        if (GetRocks() <= 1)
        {
            bool validPosition = false;
            int count = 0;
            while (validPosition == false && count < 10) { 
                validPosition = true;
                count++;
                existingTiles = LevelBar.instance.GetExistingTiles();
                tileIndex = Random.Range(0, existingTiles.Count);
                float x = existingTiles[tileIndex].x + 0.5f;
                float y = existingTiles[tileIndex].y + 0.5f;

                Vector3 spawn = new Vector3(x, y, 0);

                Collider2D[] colliders = Physics2D.OverlapCircleAll(spawn, obstacleCheckRadius);

                foreach (Collider2D col in colliders)
                {
                    // If this collider is tagged "Obstacle"
                    if (col.CompareTag("Obstacle") || col.CompareTag("Rock") || col.CompareTag("Building")) //  col.IsTouchingLayers(10)) THIS DOESNT WORK IT WOULD HAVE TO
                                                                                                               //TAKE A OBSTACLE LAYER AS A PARAM and have it referenced in the inspector IE AxeChopping script
                    {
                        // Then this position is not a valid spawn position
                        validPosition = false;
                    }
                }

                if (validPosition == true)
                {
                    Instantiate(resources[0], spawn, Quaternion.identity);
                    AdjustRocks(1);

                }
            }
        }
    }

    //------------- THIS IS TO VIEW THE OVERLAPCIRCLE ------------
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(rockPrefab.transform.position, obstacleCheckRadius);
    //}

    public void AdjustRocks(int value)
    {
        quantities[0] += value;
    }

    public int GetRocks()
    {
        return quantities[0];
    }
}
