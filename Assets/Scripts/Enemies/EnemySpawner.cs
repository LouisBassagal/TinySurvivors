using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public List<GameObject> enemyToSpawn;
    public float spawnInterval = 5f;
    public Vector2 enemyToSpawnRange = new (5, 10);
    private float spawnTimer;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemies();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemies()
    {
        int enemiesCount = Random.Range((int)enemyToSpawnRange.x, (int)enemyToSpawnRange.y + 1);
        Vector3 cameraBounds = CameraController.Instance.GetCameraBounds();

        for (int i = 0; i < enemiesCount; i++)
        {
            int enemyIndex = Random.Range(0, enemyToSpawn.Count);
            int randomX = Random.Range(0, 11);
            int randomY = Random.Range(0, 11);
            Vector3 spawnPosition = GetPositionOutsideCameraBounds();
            Instantiate(enemyToSpawn[enemyIndex], spawnPosition, Quaternion.identity);
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
}
