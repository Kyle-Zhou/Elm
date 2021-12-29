using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{

    public static GameHandler instance;

    public GameObject loadingScreen;
    public ProgressBar progressBar;

    public Text percentText;

    public void Awake()
    {
        instance = this;

        //Scene currentScene = SceneManager.GetActiveScene();
        //int buildIndex = currentScene.buildIndex;
        //if (buildIndex == (int)SceneIndexes.MANAGER) { 
            SceneManager.LoadSceneAsync((int)SceneIndexes.MENU, LoadSceneMode.Additive); //load main menu
        //}
    }


    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame()
    {

        loadingScreen.gameObject.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MENU));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.GAME, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());

    }


    public void LoadMenu()
    {
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MENU, LoadSceneMode.Single));
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)SceneIndexes.MENU));

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.GAME));



    }

    float totalSceneProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {

                totalSceneProgress = 0;

                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;
                progressBar.current = Mathf.RoundToInt(totalSceneProgress);
                //percentText.text =  "Generating Map... " + Mathf.RoundToInt(totalSceneProgress).ToString() + "%";
                //StartCoroutine(Loading());

                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);

    }

    public IEnumerator Loading()
    {
        while (true)
        {
            percentText.text = "Generating Map.";
            yield return new WaitForSeconds(0.5f);
            percentText.text = "Generating Map..";
            yield return new WaitForSeconds(0.5f);
            percentText.text = "Generating Map...";
            yield return new WaitForSeconds(0.5f);
        }
    }


}