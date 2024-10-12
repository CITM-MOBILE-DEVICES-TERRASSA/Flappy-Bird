using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesPool : MonoBehaviour
{
    [Header("Pipes Pool Settings")]
    [SerializeField] private int poolSize = 5;

    private List<GameObject> topPipesPool;
    private List<GameObject> bottomPipesPool;

    private int pipeIndex = 0;

    private void Awake()
    {
        pipeIndex = PlayerPrefs.GetInt("BackgroundIndex", 0);
        topPipesPool = new List<GameObject>();
        bottomPipesPool = new List<GameObject>();
    }

    public void InitializePipes(GameObject[] topPipePrefabs, GameObject[] bottomPipePrefabs)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject topPipe = Instantiate(topPipePrefabs[pipeIndex]);
            topPipe.SetActive(false);
            topPipesPool.Add(topPipe);

            GameObject bottomPipe = Instantiate(bottomPipePrefabs[pipeIndex]);
            bottomPipe.SetActive(false);
            bottomPipesPool.Add(bottomPipe);
        }
    }

    public GameObject GetTopPipe()
    {
        foreach (var pipe in topPipesPool)
        {
            if (!pipe.activeInHierarchy)
            {
                pipe.SetActive(true);
                return pipe;
            }
        }
        return null;
    }

    public GameObject GetBottomPipe()
    {
        foreach (var pipe in bottomPipesPool)
        {
            if (!pipe.activeInHierarchy)
            {
                pipe.SetActive(true);
                return pipe;
            }
        }
        return null;
    }

    public void ReturnPipe(GameObject pipe)
    {
        pipe.SetActive(false);
    }
}