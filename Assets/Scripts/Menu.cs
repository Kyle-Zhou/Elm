using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public static Menu instance;
    public GameObject menu;
    public GameObject pauseScreen;
    private bool pauseOpen = false;

    private bool anyMenuOpen;

    public void Start()
    {
        instance = this;
        anyMenuOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SoundManager.PlaySound(SoundManager.Sound.backpackOpen);

            menu.SetActive(true);
            OpenBackpack.instance.TurnOnBackpack();
        } else if (Input.GetKeyDown(KeyCode.Escape)) {

            if (anyMenuOpen == false) { 
                if (pauseOpen == false) {
                    Pause();
                } else if (pauseOpen == true)
                {
                    Resume();
                }
            }
        }
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        pauseOpen = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        pauseOpen = false;
        Time.timeScale = 1f;
    }

    public void SetAnyMenuOpen(bool o)
    {
        anyMenuOpen = o;
    }

    //public void FreezeTime()
    //{
    //    Time.timeScale = 0f;
    //}

    //public void UnFreezeTime()
    //{
    //    Time.timeScale = 1f;
    //}


}
