using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("Configuração Básica")]
    public GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxEnemyScreen = 5;
    public int limiteTotalInimigos = 10;

    [Header("Controle de Estado (Apenas Leitura)")]
    public int enemyCount = 0;
    public int totalSpawnados = 0;
    public bool isCleared = false;

    [Header("Pontos de Spawn")]
    public Transform[] spawnsPoints;

    private bool isSpawning = false;

    void Update()
    {
        if (totalSpawnados < limiteTotalInimigos && enemyCount < maxEnemyScreen && !isSpawning)
        {
            StartCoroutine(SpawnManager());
        }

        if (totalSpawnados >= limiteTotalInimigos && enemyCount == 0 && !isCleared)
        {
            isCleared = true;
        }
    }

    public Vector3 DefineSpawn()
    {
        if (spawnsPoints == null || spawnsPoints.Length == 0)
        {
            return transform.position;
        }

        int randomIndex = Random.Range(0, spawnsPoints.Length);

        return spawnsPoints[randomIndex].position;
    }

    public IEnumerator SpawnManager()
    {
        isSpawning = true;

        Instantiate(enemyPrefab, DefineSpawn(), Quaternion.identity);

        enemyCount++;
        totalSpawnados++;

        yield return new WaitForSeconds(spawnInterval);

        isSpawning = false;
    }

    public void EnemyDeathManager()
    {
        if (enemyCount > 0)
        {
            enemyCount--;
        }
    }
}