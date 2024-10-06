using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Sprite[] numberSprites;
    [SerializeField] private GameObject digitPrefab;
    [SerializeField] private Transform panel;
    [Header("Game Over Score")]
    [SerializeField] private GameObject[] medals;
    [SerializeField] private GameObject smallDigitPrefab;
    [SerializeField] private Transform scorePanel;
    [SerializeField] private Transform bestScorePanel;

    private AudioSource audioSource;

    private int score = 0;
    private int medalsIndex = -1; // 0 = bronze, 1 = silver, 2 = gold, 3 = platinum

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateScoreUI(score, digitPrefab, panel);
    }

    private void UpdateScoreUI(int newScore, GameObject digit, Transform newPanel)
    {
        string scoreString = newScore.ToString();
        int currentDigitCount = newPanel.childCount;

        if (scoreString.Length > currentDigitCount)
        {
            for (int i = currentDigitCount; i < scoreString.Length; i++)
            {
                Instantiate(digit, newPanel);
            }
        }

        for (int i = 0; i < scoreString.Length; i++)
        {
            int digitValue = int.Parse(scoreString[i].ToString());
            newPanel.GetChild(i).GetComponent<Image>().sprite = numberSprites[digitValue];
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        audioSource.Play();
        UpdateScoreUI(score, digitPrefab, panel);
    }

    public void GameOverScore()
    {
        if (score >= 0 && score < 10)
        {
            medalsIndex = -1;
        }
        else if (score >= 10 && score < 20)
        {
            medalsIndex = 0;
            PlayerPrefs.SetInt("BronzeMedal", PlayerPrefs.GetInt("BronzeMedal", 0) + 1);
        }
        else if (score >= 20 && score < 30)
        {
            medalsIndex = 1;
            PlayerPrefs.SetInt("SilverMedal", PlayerPrefs.GetInt("SilverMedal", 0) + 1);
        }
        else if (score >= 30 && score < 40)
        {
            medalsIndex = 2;
            PlayerPrefs.SetInt("GoldMedal", PlayerPrefs.GetInt("GoldMedal", 0) + 1);
        }
        else if (score >= 40)
        {
            medalsIndex = 3;
            PlayerPrefs.SetInt("PlatinumMedal", PlayerPrefs.GetInt("PlatinumMedal", 0) + 1);
        }

        if (medalsIndex != -1)
            medals[medalsIndex].SetActive(true);

        UpdateScoreUI(score, smallDigitPrefab, scorePanel);
        if (score > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        UpdateScoreUI(PlayerPrefs.GetInt("BestScore", 0), smallDigitPrefab, bestScorePanel);

        panel.gameObject.SetActive(false);
    }
}
