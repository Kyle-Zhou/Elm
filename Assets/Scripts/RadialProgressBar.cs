using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    public static RadialProgressBar instance;
    public GameObject targetObject;
    private float fillIncrementValue = 0.004f;
    private float timeIncrementValue;
    public Image fill;
    private float fillAm;
    private bool run = false;
    private float startTime;
    public GameObject menu2;
    private AudioSource audioSource;

    public void Start()
    {
        instance = this;
    }

    public void SetRun(bool r)
    {
        run = r;
        run = true; //should only be for setting run true;
        startTime = Time.time;
        fillAm = 0;
        targetObject.GetComponent<Animator>().SetBool("Cooking", true);
        targetObject.transform.GetChild(0).gameObject.SetActive(true);

        audioSource = SoundManager.PlayLoopedSound(SoundManager.Sound.bubbling);

    }

    public void SetTimeIncrementValue(float t)
    {
        timeIncrementValue = t;
    }

    public bool GetRun()
    {
        return run;
    }

    public void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(targetObject.transform.position + new Vector3(0, 1, 0));

        if (run)
        {

            if (Time.time >= startTime + timeIncrementValue)
            {
                fillAm += fillIncrementValue;
                fill.fillAmount = fillAm;

                if (fillAm >= 1)
                {
                    run = false;
                    fill.fillAmount = 0;
                    targetObject.GetComponent<Animator>().SetBool("Cooking", false);
                    targetObject.transform.GetChild(0).gameObject.SetActive(false);

                    SoundManager.StopLoopedSound(audioSource);

                    gameObject.transform.parent.gameObject.SetActive(false);
                    menu2.GetComponent<CollectFood>().EnableCollectButton();

                }
            }

        }
    }

}