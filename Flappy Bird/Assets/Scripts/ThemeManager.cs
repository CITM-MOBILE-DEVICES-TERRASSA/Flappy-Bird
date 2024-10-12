using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] private GameObject[] background;
    [Header("Birds")]
    [SerializeField] private GameObject[] birds;
    [SerializeField] private Vector3 birdPosition = new Vector3(-1, 1, 0);
    [SerializeField] private bool instantiateBird = true;

    private static int backgroundIndex = 0; // 0: day, 1: night
    private static int birdIndex = 0; // 0: yellow, 1: blue, 2: red

    private void Start()
    {
        GetBackgroundInt();
        GetBirdInt();

        SpawnBackground();
        SpawnBird();
    }

    public void SpawnBird()
    {
        GetBirdInt();

        if (instantiateBird)
            Instantiate(birds[birdIndex], birdPosition, Quaternion.identity);
        else
            birds[birdIndex].SetActive(true);
    }

    public void SpawnBackground()
    {
        GetBackgroundInt();

        background[backgroundIndex].SetActive(true);
    }

    public void ChangeBackground(int index)
    {
        background[backgroundIndex].SetActive(false);
        backgroundIndex = index;
        PlayerPrefs.SetInt("BackgroundIndex", backgroundIndex);
        background[backgroundIndex].SetActive(true);
    }

    public void ChangeBird(int index)
    {
        birds[birdIndex].SetActive(false);
        birdIndex = index;
        PlayerPrefs.SetInt("BirdIndex", birdIndex);
        birds[birdIndex].SetActive(true);
    }

    private void GetBirdInt()
    {
        if (PlayerPrefs.HasKey("BirdIndex"))
            birdIndex = PlayerPrefs.GetInt("BirdIndex");
    }

    private void GetBackgroundInt()
    {
        if (PlayerPrefs.HasKey("BackgroundIndex"))
            backgroundIndex = PlayerPrefs.GetInt("BackgroundIndex");
    }
}
