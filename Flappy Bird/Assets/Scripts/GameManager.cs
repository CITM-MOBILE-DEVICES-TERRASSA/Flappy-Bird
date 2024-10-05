using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject tapInfo;
    [SerializeField] private GameObject gameOverInfo;
    [SerializeField] private GameObject pause;
    [Header("Background")]
    [SerializeField] private GameObject[] background;
    [Header("Birds")]
    [SerializeField] private GameObject[] birds;
    [SerializeField] private Vector3 birdPosition = new Vector3(-1, 1, 0);
    [SerializeField] private bool instantiateBird = true;

    private AudioSource audioSource;
    private PipesManager pipesManager;

    private bool isGameStarted = false;
    private bool isGameActive = true;
    private static int backgroundIndex = 0; // 0: day, 1: night
    private static int birdIndex = 0; // 0: yellow, 1: blue, 2: red

    public bool IsGameStarted() => isGameStarted;
    public bool IsGameActive() => isGameActive;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        pipesManager = FindObjectOfType<PipesManager>();

        background[backgroundIndex].SetActive(true);

        if (instantiateBird)
            Instantiate(birds[birdIndex], birdPosition, Quaternion.identity);
        if (pipesManager != null)
            pipesManager.SetPipeIndex(backgroundIndex);
    }

    public void ChangeBackground(int index)
    {
        background[backgroundIndex].SetActive(false);
        backgroundIndex = index;
        background[backgroundIndex].SetActive(true);
        if (pipesManager != null)
            pipesManager.SetPipeIndex(backgroundIndex);
    }

    public void ChangeBird(int index)
    {
        birds[birdIndex].SetActive(false);
        birdIndex = index;
        birds[birdIndex].SetActive(true);
    }

    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        isGameStarted = true;
        tapInfo.SetActive(false);
        StartCoroutine(pipesManager.SpawnPipes());
    }

    public void FinishGame()
    {
        isGameActive = false;
        pause.gameObject.SetActive(false);
        pipesManager.StopAllCoroutines();
        gameOverInfo.SetActive(true);
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
