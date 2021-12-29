using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    private float DestroyTime = 3.0f;

    private Vector3 offset = new Vector3(0, 1, 0);

    private Vector3 randomizeIntensity = new Vector3(0.5f, 0 ,0);

    // Start is called before the first frame update
    void Start()
    {

        GetComponent<Renderer>().sortingLayerID = 2;
        GetComponent<Renderer>().sortingOrder = 0;

        if (gameObject.name != "FloatingTextBase") { 
            Destroy(gameObject, DestroyTime);
        }
        transform.localPosition += offset;

        transform.localPosition += new Vector3(
        Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
        Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
        Random.Range(-randomizeIntensity.z, randomizeIntensity.z));

    }




}
