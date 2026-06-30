using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float mapSize = 20f;
    [SerializeField] private float spawnOffset = 2f;

    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int maxEnemies = 5;

    [SerializeField] private UIEvents uiEvents;

    private readonly List<Enemy> activeEnemies = new();

    private float timer;

    private void Start()
    {
        uiEvents = FindFirstObjectByType<UIEvents>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            if (activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Vector3 position = GetSpawnPosition();

        GameObject enemyObject =
            Instantiate(enemyPrefab, position, Quaternion.identity);

        Enemy enemy =
            enemyObject.GetComponent<Enemy>();

        activeEnemies.Add(enemy);
        uiEvents.EnemySpawned();
        enemy.OnDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
        uiEvents.EnemyDefeated();
    }

    private Vector3 GetSpawnPosition()
    {
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                return new Vector3(
                    Random.Range(-mapSize, mapSize),
                    0,
                    mapSize + spawnOffset);

            case 1:
                return new Vector3(
                    Random.Range(-mapSize, mapSize),
                    0,
                    -mapSize - spawnOffset);

            case 2:
                return new Vector3(
                    -mapSize - spawnOffset,
                    0,
                    Random.Range(-mapSize, mapSize));

            default:
                return new Vector3(
                    mapSize + spawnOffset,
                    0,
                    Random.Range(-mapSize, mapSize));
        }
    }
}