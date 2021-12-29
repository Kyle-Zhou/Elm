using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolInventory : MonoBehaviour
{
    public static ToolInventory instance;

    public List<GameObject> tools;
    public Image baseImage;
    public List<Sprite> hotbars;
    public Camera cam;

    private static int currentTool;

    private void Start()
    {
        //change this later when currentTool is saved to player prefs.
        instance = this;
        currentTool = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           SwitchTool(1);
           baseImage.sprite = hotbars[0];
           cam.GetComponent<CameraController>().SetWield(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchTool(2);
            baseImage.sprite = hotbars[1];
            cam.GetComponent<CameraController>().SetWield(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchTool(3);
            baseImage.sprite = hotbars[2];
            cam.GetComponent<CameraController>().SetWield(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchTool(4);
            baseImage.sprite = hotbars[3];
            cam.GetComponent<CameraController>().SetWield(true);
        } 
        //} else if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    SwitchTool(5);
        //    baseImage.sprite = hotbars[4];
        //    cam.GetComponent<CameraController>().SetWield(true);
        //}
    }

    public void SwitchTool(int tool)
    {
        currentTool = tool;

        switch (tool)
        {
            case 1:
                tools[0].SetActive(true);
                tools[1].SetActive(false);
                tools[2].SetActive(false);
                tools[3].SetActive(false);
                //tools[4].SetActive(false);
                break;
            case 2:
                tools[1].SetActive(true);
                tools[2].SetActive(false);
                tools[3].SetActive(false);
                //tools[4].SetActive(false);
                tools[0].SetActive(false);
                break;
            case 3:
                tools[2].SetActive(true);
                tools[3].SetActive(false);
                //tools[4].SetActive(false);
                tools[0].SetActive(false);
                tools[1].SetActive(false);
                break;
            case 4:
                tools[3].SetActive(true);
                //tools[4].SetActive(false);
                tools[0].SetActive(false);
                tools[1].SetActive(false);
                tools[2].SetActive(false);
                break;
            //case 5:
            //    tools[4].SetActive(true);
            //    tools[0].SetActive(false);
            //    tools[1].SetActive(false);
            //    tools[2].SetActive(false);
            //    tools[3].SetActive(false);
            //    break;
        }
    }

    public int GetCurrentTool() {
        return currentTool;
    }
}
