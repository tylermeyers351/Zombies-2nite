using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] float spawnTimer = 5f;
    [SerializeField] float reduceTimer = 0.1f;
    [SerializeField] float minimumTimer = 1f;

    [SerializeField] GameObject zombiePrefab;
    
    PlayerHealth player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (player)
        {
            Instantiate(zombiePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTimer);
            spawnTimer = Mathf.Max(minimumTimer, spawnTimer - reduceTimer);
        }
    }
}
