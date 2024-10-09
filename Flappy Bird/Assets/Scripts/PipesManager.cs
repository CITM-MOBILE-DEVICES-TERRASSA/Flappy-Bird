using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesManager : MonoBehaviour
{
    [Header("Pipes Pool Reference")]
    [SerializeField] private PipesPool pipesPool;

    [Header("Pipes Prefabs")]
    [SerializeField] private GameObject[] topPipePrefabs;
    [SerializeField] private GameObject[] bottomPipePrefabs;

    [Header("Pipes Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnPositionX = 5f;
    [SerializeField] private float maxHeight = 1f;   
    [SerializeField] private float pipesDistance = 7f;

    public int SetPipeIndex(int index) => pipesPool.SetPipeIndex(index);

    private void Awake()
    {
        pipesPool.InitializePipes(topPipePrefabs, bottomPipePrefabs);
    }

    public IEnumerator SpawnPipes()
    {
        while (true)
        {
            float randomHeight = Random.Range(maxHeight - 1f, maxHeight + 1f);

            Vector3 topSpawnPosition = new Vector3(spawnPositionX, randomHeight, 0);
            GameObject topPipe = pipesPool.GetTopPipe();
            if (topPipe != null)
            {
                topPipe.transform.position = topSpawnPosition;
            }

            float bottomPipeHeight = randomHeight - pipesDistance;
            Vector3 bottomSpawnPosition = new Vector3(spawnPositionX, bottomPipeHeight, 0);
            GameObject bottomPipe = pipesPool.GetBottomPipe();
            if (bottomPipe != null)
            {
                bottomPipe.transform.position = bottomSpawnPosition;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
