using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MedalManager : MonoBehaviour
{
    [Header("Medals")]
    [SerializeField] private TextMeshProUGUI bronzeMedalText;
    [SerializeField] private TextMeshProUGUI silverMedalText;
    [SerializeField] private TextMeshProUGUI goldMedalText;
    [SerializeField] private TextMeshProUGUI platinumMedalText;
   
    public void CheckUpdate()
    {
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
}
