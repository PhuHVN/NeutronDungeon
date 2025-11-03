using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [HideInInspector] public RoomController room;

    public List<Transform> spawnPoints;
    public GameObject[] enemyPrefabs;
    private int enemyCount;
    private bool isActive = false;

    public void StartWave()
    {
        if (isActive) return;
        isActive = true;

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        enemyCount = spawnPoints.Count;

        foreach (var point in spawnPoints)
        {
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy = Instantiate(enemyPrefabs[rand], point.position, Quaternion.identity, room.transform);
            enemy.gameObject.SetActive(true);
        
            Enemy e = enemy.GetComponent<Enemy>();
            e.onDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            room.OnWaveCleared();
        }
    }

    public void EnemyHasDied()
    {
        enemyCount--;
        Debug.Log("An enemy died! Enemies remaining: " + enemyCount);
        if (enemyCount <= 0)
        {
            Debug.Log("WAVE CLEARED! Telling RoomController...");
            room.OnWaveCleared();
        }
    }
}
