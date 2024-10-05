using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform tapInfo;
    [SerializeField] private Transform gameOverInfo;

    private PipesManager pipesManager;

    private bool isGameStarted = false;
    private bool isGameActive = true;

    public bool IsGameStarted() => isGameStarted;
    public bool IsGameActive() => isGameActive;

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

    public void FinishGame()
    {
        isGameActive = false;
        pipesManager.StopAllCoroutines();
        gameOverInfo.gameObject.SetActive(true);
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
