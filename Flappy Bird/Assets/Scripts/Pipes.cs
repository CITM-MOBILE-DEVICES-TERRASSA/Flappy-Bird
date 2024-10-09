using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField] private float velocity = 2f;
    [SerializeField] private float destroyPosition = -5f;

    private GameManager gameManager;
    private PipesPool pipesPool;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        pipesPool = FindObjectOfType<PipesPool>();
    }

    private void Update()
    {
        var collider = GetComponent<BoxCollider2D>();

        if (gameManager.IsGameActive())
        {
            if (!collider.enabled)
                collider.enabled = true;

            transform.position -= new Vector3(velocity, 0, 0) * Time.deltaTime;

            if (transform.position.x < destroyPosition)
            {
                pipesPool.ReturnPipe(gameObject);
            }
        }
        else
        {
            if (collider.enabled)
                collider.enabled = false;
        }
    }
}