using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform tapInfo;

    private PipesManager pipesManager;

    private bool isGameStarted = false;
    private bool isGameActive = true;

    public bool IsGameStarted() => isGameStarted;

    private void Start()
    {
        pipesManager = FindObjectOfType<PipesManager>();
    }

    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        isGameStarted = true;
        tapInfo.gameObject.SetActive(false);
        StartCoroutine(pipesManager.SpawnPipes());
    }

    public void PauseGame()
    {
        if (isGameActive)
        {
            Time.timeScale = 0;
            isGameActive = false;
        }
        else
        {
            Time.timeScale = 1;
            isGameActive = true;
        }
    }
}
