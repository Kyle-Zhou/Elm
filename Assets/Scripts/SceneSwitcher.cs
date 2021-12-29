using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{

    public GameObject wallTileMap;
    public GameObject ledgeTileMap;
    public GameObject grassTileMap;

    public GameObject roomRoot;

    public void Start()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
    }

    public void GoToMainScene()
    {
        //SceneManager.LoadScene((int)SceneIndexes.GAME);
        GameHandler.instance.LoadGame();
    }

    //public void GoToMenuScene()
    //{
    //    Time.timeScale = 1f; //resets the time for when player goes back
    //    SceneManager.LoadScene()
    //}

    public void GoToMenuScene2()
    {
        ClearTiles();
        Time.timeScale = 1f;
        SceneManager.LoadScene((int)SceneIndexes.MANAGER);
        //GameHandler.instance.LoadMenu();
    }

    public void GoToMainScene2()
    {
        //SceneManager.LoadScene((int)SceneIndexes.GAME);
        GameHandler.instance.LoadGame();

    }

    public void ClearTiles()
    {

        roomRoot.AddComponent<RoomInstance>().ClearStaticLists();
    }

    public void Quit()
    {
        Application.Quit();
    }

}