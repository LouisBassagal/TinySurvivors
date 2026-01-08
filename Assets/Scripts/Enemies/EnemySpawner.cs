using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Spawn Settings")]
    public List<GameObject> enemyToSpawn;
    public float spawnInterval = 5f;
    public Vector2 enemyToSpawnRange = new (5, 10);
    public int maxEnemies = 50;
    private float m_spawnTimer;
    private readonly List<GameObject> m_spawnedEnemies = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        m_spawnTimer += Time.deltaTime;
        if (m_spawnTimer >= spawnInterval && m_spawnedEnemies.Count < maxEnemies)
        {
            SpawnEnemies();
            m_spawnTimer = 0f;
        }
    }

    private void SpawnEnemies()
    {
        int enemiesCount = Random.Range((int)enemyToSpawnRange.x, (int)enemyToSpawnRange.y + 1);

        for (int i = 0; i < enemiesCount; i++)
        {
            int enemyIndex = Random.Range(0, enemyToSpawn.Count);
            Vector3 spawnPosition = GetPositionOutsideCameraBounds();
            GameObject spawnedEnemy = Instantiate(enemyToSpawn[enemyIndex], spawnPosition, Quaternion.identity);
            m_spawnedEnemies.Add(spawnedEnemy);
        }
    }

    private Vector3 GetPositionOutsideCameraBounds()
    {
        var side = Random.Range(0, 4);
        Vector3 cameraBounds = CameraController.Instance.GetCameraBounds();
        Vector3 spawnPosition = CameraController.Instance.GetCameraCenter();
        spawnPosition.z = 0;

        switch (side)
        {
            case 0: // Top
                spawnPosition += new Vector3(Random.Range(-cameraBounds.x / 2, cameraBounds.x / 2), cameraBounds.y / 2 + 1, 0);
                break;
            case 1: // Bottom
                spawnPosition += new Vector3(Random.Range(-cameraBounds.x / 2, cameraBounds.x / 2), -cameraBounds.y / 2 - 1, 0);
                break;
            case 2: // Left
                spawnPosition += new Vector3(-cameraBounds.x / 2 - 1, Random.Range(-cameraBounds.y / 2, cameraBounds.y / 2), 0);
                break;
            case 3: // Right
                spawnPosition += new Vector3(cameraBounds.x / 2 + 1, Random.Range(-cameraBounds.y / 2, cameraBounds.y / 2), 0);
                break;
        }
        return spawnPosition;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (m_spawnedEnemies.Contains(enemy))
        {
            m_spawnedEnemies.Remove(enemy);
        }
    }
}
