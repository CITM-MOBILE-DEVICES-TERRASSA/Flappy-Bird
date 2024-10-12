using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject tapInfo;
    [SerializeField] private GameObject gameOverInfo;
    [SerializeField] private GameObject pause;
    
    public void Begin()
    {
        tapInfo.SetActive(false);
        pause.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        pause.gameObject.SetActive(false);
        gameOverInfo.SetActive(true);
    }

    public void PauseGameObject() 
    {
        if (pause != null)
            pause.gameObject.SetActive(false);
    }
}
