using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatTexture : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float speed = 0.5f;

    void Update()
    {
        Rect uvRect = rawImage.uvRect;
        uvRect.x += speed * Time.deltaTime;
        uvRect.x = Mathf.Repeat(uvRect.x, 1f);
        rawImage.uvRect = uvRect;
    }
}
