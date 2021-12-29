using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{

    public Text percentText;
    private float waitTime = 1f;
    private float newTime;
    private int count = 1;

    void Start()
    {
        StartCoroutine(Loading());
    }

    public IEnumerator Loading()
    {
        while (true) { 
            percentText.text = "Generating Map.";
            yield return new WaitForSeconds(0.5f);
            percentText.text = "Generating Map..";
            yield return new WaitForSeconds(0.5f);
            percentText.text = "Generating Map...";
            yield return new WaitForSeconds(0.5f);
        }
    }

}
