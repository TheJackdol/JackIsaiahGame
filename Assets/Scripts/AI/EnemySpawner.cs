using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnCooldown = 5f;

    private float lastSpawnTime;

    void Update()
    {
        if (Time.time > lastSpawnTime + spawnCooldown)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Enemy spawned!");
    }
}