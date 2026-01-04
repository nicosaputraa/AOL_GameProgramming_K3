using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float respawnTime = 5f;
    public Transform[] spawnPoints;

    GameObject currentEnemy;

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = transform.position;

        if (spawnPoints != null && spawnPoints.Length > 0)
            spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        currentEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        EnemyAI2D ai = currentEnemy.GetComponent<EnemyAI2D>();
        if (ai != null)
            ai.spawner = this;
    }

    public void OnEnemyDied()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnEnemy();
    }
}
