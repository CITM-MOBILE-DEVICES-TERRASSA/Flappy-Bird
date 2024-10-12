using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private AudioSource audioSource;
    private PipesManager pipesManager;
    private UIManager uiManager;
    private ThemeManager themeManager;
    private MedalManager medalManager;

    private bool isGameStarted = false;
    private bool isGameActive = true;
    private static int backgroundIndex = 0; // 0: day, 1: night

    public bool IsGameStarted() => isGameStarted;
    public bool IsGameActive() => isGameActive;

    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        themeManager = FindObjectOfType<ThemeManager>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        themeManager.SpawnBackground();
        themeManager.SpawnBird();
    }

    public void StartGame()
    {
        isGameStarted = true;
        uiManager.Begin();
        pipesManager.StartSpawningPipes();
    }

    public void FinishGame()
    {
        isGameActive = false;
        uiManager.GameOver();
        pipesManager.StopSpawningPipes();
        audioSource.Play();
    }

    public void PauseGame()
    {
        if (isGameActive)
        {
            Time.timeScale = 0;
            isGameActive = false;
            MusicManager.Instance.PauseMusic();
        }
        else
        {
            Time.timeScale = 1;
            isGameActive = true;
            MusicManager.Instance.PlayMusic();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isGameActive = true;
        isGameStarted = false;
        
        if (scene.name == "Menu")
        {
            medalManager = FindObjectOfType<MedalManager>();
            medalManager.CheckUpdate();
        }
        else if (scene.name == "Gameplay")
        {
            uiManager = FindObjectOfType<UIManager>();
            pipesManager = FindObjectOfType<PipesManager>();
        }
    }
}
