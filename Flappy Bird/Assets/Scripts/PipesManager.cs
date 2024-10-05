using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameObject topPipePrefab;
    [SerializeField] private GameObject bottomPipePrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnPositionX = 5f;
    [SerializeField] private float minHeight = -1f; 
    [SerializeField] private float maxHeight = 1f;   
    [SerializeField] private float pipesDistance = 7f;

    private void Start()
    {
        StartCoroutine(SpawnPipes());
    }

    private IEnumerator SpawnPipes()
    {
        while (true)
        {
            float randomHeight = Random.Range(maxHeight - 1f, maxHeight + 1f);

            Vector3 topSpawnPosition = new Vector3(spawnPositionX, randomHeight, 0);
            Instantiate(topPipePrefab, topSpawnPosition, Quaternion.identity);

            float bottomPipeHeight = randomHeight - pipesDistance;
            Vector3 bottomSpawnPosition = new Vector3(spawnPositionX, bottomPipeHeight, 0);
            Instantiate(bottomPipePrefab, bottomSpawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
