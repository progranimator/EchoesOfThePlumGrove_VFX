using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    public GameObject butterflyPrefab;
    public float spawnInterval = 1f;
    public float spawnRadius = 1f;
    public GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnButterflies());
    }

    IEnumerator SpawnButterflies()
    {
        while (true)
        {
            Vector3 randomPosition = spawnPoint.transform.position + new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0f,
                Random.Range(-spawnRadius, spawnRadius)
            );
            Instantiate(butterflyPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}