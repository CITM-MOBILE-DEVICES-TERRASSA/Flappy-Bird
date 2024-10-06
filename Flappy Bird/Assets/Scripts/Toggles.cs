using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggles : MonoBehaviour
{
    [Header("Toggles")]
    [SerializeField] private Toggle[] backgroundToggle;
    [SerializeField] private Toggle[] birdToggle;

    public void UpdateToggles()
    {
        if (PlayerPrefs.HasKey("BackgroundIndex"))
            backgroundToggle[PlayerPrefs.GetInt("BackgroundIndex")].isOn = true;
        if (PlayerPrefs.HasKey("BirdIndex"))
        birdToggle[PlayerPrefs.GetInt("BirdIndex")].isOn = true;
    }
}
