using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatTexture : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float baseSpeed = 0.5f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gameManager.IsGameActive())
            return;
            
        float aspectRatio = (float)Screen.height / Screen.width;
        float targetAspectRatio = 16 / 9f;
        float speed = baseSpeed * (aspectRatio / targetAspectRatio);
        Rect uvRect = rawImage.uvRect;
        uvRect.x += speed * Time.deltaTime;
        uvRect.x = Mathf.Repeat(uvRect.x, 1f);
        rawImage.uvRect = uvRect;
    }
}
