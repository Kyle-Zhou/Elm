using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Entrance : MonoBehaviour
{
    public bool active;
    public ParticleSystem fog, ps;
    public GameObject prompt;

    private void Update()
    {
        active = false;

        foreach (Transform child in gameObject.transform.parent)
        {
            if (child.CompareTag("Mob") || child.CompareTag("Gatekeeper"))
            {
                active = true;
                break; //For efficiency.
            }
        }

        if (!active)
        {
            ps.Stop();
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
