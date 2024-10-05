using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Sprite[] numberSprites;
    [SerializeField] private GameObject digitPrefab;
    [SerializeField] private Transform panel;

    private AudioSource audioSource;

    private int score = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        audioSource.Play();
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        string scoreString = score.ToString();
        int currentDigitCount = panel.childCount;

        if (scoreString.Length > currentDigitCount)
        {
            for (int i = currentDigitCount; i < scoreString.Length; i++)
            {
                Instantiate(digitPrefab, panel);
            }
        }

        for (int i = 0; i < scoreString.Length; i++)
        {
            int digitValue = int.Parse(scoreString[i].ToString());
            panel.GetChild(i).GetComponent<Image>().sprite = numberSprites[digitValue];
        }
    }
}
