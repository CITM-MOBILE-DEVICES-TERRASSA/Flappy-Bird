using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [Header("Medals")]
    [SerializeField] private TextMeshProUGUI bronzeMedalText;
    [SerializeField] private TextMeshProUGUI silverMedalText;
    [SerializeField] private TextMeshProUGUI goldMedalText;
    [SerializeField] private TextMeshProUGUI platinumMedalText;

    private AudioSource audioSource;
    private PipesManager pipesManager;
    private Coroutine spawnPipesCoroutine;

    private bool isGameStarted = false;
    private bool isGameActive = true;
    private static int backgroundIndex = 0; // 0: day, 1: night
    private static int birdIndex = 0; // 0: yellow, 1: blue, 2: red

    public bool IsGameStarted() => isGameStarted;
    public bool IsGameActive() => isGameActive;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (pause != null)
            pause.gameObject.SetActive(false);
    }

    private void Start()
    {
        pipesManager = FindObjectOfType<PipesManager>();

        if (PlayerPrefs.HasKey("BackgroundIndex"))
            backgroundIndex = PlayerPrefs.GetInt("BackgroundIndex");
        if (PlayerPrefs.HasKey("BirdIndex"))
            birdIndex = PlayerPrefs.GetInt("BirdIndex");

        if (instantiateBird)
            Instantiate(birds[birdIndex], birdPosition, Quaternion.identity);
        else
            birds[birdIndex].SetActive(true);
            
        background[backgroundIndex].SetActive(true);

        if (pipesManager != null)
            pipesManager.SetPipeIndex(backgroundIndex);

        if (bronzeMedalText != null && silverMedalText != null && goldMedalText != null && platinumMedalText != null)
            UpdateMedalText();
    }

    private void UpdateMedalText()
    {
        bronzeMedalText.text = PlayerPrefs.GetInt("BronzeMedal", 0).ToString();
        silverMedalText.text = PlayerPrefs.GetInt("SilverMedal", 0).ToString();
        goldMedalText.text = PlayerPrefs.GetInt("GoldMedal", 0).ToString();
        platinumMedalText.text = PlayerPrefs.GetInt("PlatinumMedal", 0).ToString();
    }

    public void ChangeBackground(int index)
    {
        background[backgroundIndex].SetActive(false);
        backgroundIndex = index;
        PlayerPrefs.SetInt("BackgroundIndex", backgroundIndex);
        background[backgroundIndex].SetActive(true);
        if (pipesManager != null)
            pipesManager.SetPipeIndex(backgroundIndex);
    }

    public void ChangeBird(int index)
    {
        birds[birdIndex].SetActive(false);
        birdIndex = index;
        PlayerPrefs.SetInt("BirdIndex", birdIndex);
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
        pause.gameObject.SetActive(true);
        spawnPipesCoroutine = StartCoroutine(pipesManager.SpawnPipes());
    }

    public void FinishGame()
    {
        isGameActive = false;
        pause.gameObject.SetActive(false);
        StopCoroutine(spawnPipesCoroutine);
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
