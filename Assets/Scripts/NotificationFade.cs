using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationFade : MonoBehaviour
{
    public SpriteRenderer sr;
    private Vector3 initial;
    private bool running, revert = true;

    private void OnDisable()
    {
        sr.color = new Color(1, 1, 1, 0);
        this.transform.localPosition = initial;
    }

    private void OnEnable()
    {
        initial = this.transform.localPosition;
    }

    private void Update()
    {
        if (!running && !revert)
        {
            this.transform.localPosition = initial;
            revert = true;
        }
    }

    public IEnumerator Fade()
    {
        running = true;
        sr.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.5f);
        for (float i = 1; i >= 0; i-= 0.01f)
        {
            sr.color = new Color(1, 1, 1, i);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.0025f, this.transform.position.z);
            yield return null;
        }
        running = false;
        revert = false;
    }
}
