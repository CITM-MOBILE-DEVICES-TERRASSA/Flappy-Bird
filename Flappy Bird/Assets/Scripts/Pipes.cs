using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField] private float velocity = 2f;
    [SerializeField] private float destroyPosition = -5f;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.IsGameActive())
        {
            transform.position -= new Vector3(velocity, 0, 0) * Time.deltaTime;

            if (transform.position.x < destroyPosition)
                Destroy(gameObject);
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
