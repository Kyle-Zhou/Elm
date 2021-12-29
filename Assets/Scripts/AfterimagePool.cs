using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterimagePool : MonoBehaviour
{
    public static AfterimagePool instance;
    
    public GameObject afterimage;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    private void Start()
    {
        instance = this;
        ExpandPool();
    }

    private void ExpandPool()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject afterimageInstance = Instantiate(afterimage);
            afterimageInstance.transform.SetParent(transform);
            AppendPool(afterimageInstance);
        }
    }

    public void AppendPool(GameObject afterimageInstance)
    {
        afterimageInstance.SetActive(false);
        availableObjects.Enqueue(afterimageInstance);
    }

    public GameObject LayAfterimage()
    {
        if (availableObjects.Count == 0)
        {
            ExpandPool();
        }

        GameObject afterimageInstance = availableObjects.Dequeue();
        afterimageInstance.SetActive(true);

        return afterimageInstance;
    }
}
