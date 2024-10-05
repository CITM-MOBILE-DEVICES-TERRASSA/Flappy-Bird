using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesManager : MonoBehaviour
{
    [Header("Pipes Prefabs")]
    [SerializeField] private GameObject[] topPipePrefabs;
    [SerializeField] private GameObject[] bottomPipePrefabs;
    [Header("Pipes Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnPositionX = 5f;
    [SerializeField] private float minHeight = -1f; 
    [SerializeField] private float maxHeight = 1f;   
    [SerializeField] private float pipesDistance = 7f;

    private int pipeIndex = 0; // 0: day, 1: night

    public int SetPipeIndex(int index) => pipeIndex = index;

    public IEnumerator SpawnPipes()
    {
        while (true)
        {
            float randomHeight = Random.Range(maxHeight - 1f, maxHeight + 1f);

            Vector3 topSpawnPosition = new Vector3(spawnPositionX, randomHeight, 0);
            Instantiate(topPipePrefabs[pipeIndex], topSpawnPosition, Quaternion.identity);

            float bottomPipeHeight = randomHeight - pipesDistance;
            Vector3 bottomSpawnPosition = new Vector3(spawnPositionX, bottomPipeHeight, 0);
            Instantiate(bottomPipePrefabs[pipeIndex], bottomSpawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
