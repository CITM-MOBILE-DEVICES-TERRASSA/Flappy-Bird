using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private AudioSource audioSource;
    private PipesManager pipesManager;
    private UIManager uiManager;
    private ThemeManager themeManager;
    private MedalManager medalManager;
    private Coroutine spawnPipesCoroutine;

    private bool isGameStarted = false;
    private bool isGameActive = true;
    private static int backgroundIndex = 0; // 0: day, 1: night
    private static int birdIndex = 0; // 0: yellow, 1: blue, 2: red

    public bool IsGameStarted() => isGameStarted;
    public bool IsGameActive() => isGameActive;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        themeManager = FindObjectOfType<ThemeManager>();
        medalManager = FindObjectOfType<MedalManager>();

        audioSource = GetComponent<AudioSource>();

        if(uiManager != null)
        {
            uiManager.PauseGameObject();
        }
       
    }

    private void Start()
    {
        pipesManager = FindObjectOfType<PipesManager>();

        themeManager.SpawnBird();
        themeManager.SpawnBackground();

        if (pipesManager != null)
            pipesManager.SetPipeIndex(backgroundIndex);
        
        medalManager.CheckUpdate();
    }
    

    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        isGameStarted = true;
        uiManager.Begin();
        spawnPipesCoroutine = StartCoroutine(pipesManager.SpawnPipes());
    }

    public void FinishGame()
    {
        isGameActive = false;
        uiManager.GameOver();
        StopCoroutine(spawnPipesCoroutine);
        audioSource.Play();
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
