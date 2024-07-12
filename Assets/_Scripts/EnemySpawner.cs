using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public float spawnInterval = 5f; // Intervalo entre spawns
    public int maxEnemies = 20; // Número máximo de enemigos en la escena
    public LayerMask groundLayer; // Capa del suelo para detectar colisiones
    public float spawnRadius = 50f; // Radio en el que los enemigos pueden aparecer

    private int currentEnemyCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();

                if (spawnPosition != Vector3.zero)
                {
                    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    currentEnemyCount++;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        for (int i = 0; i < 10; i++) // Intentar encontrar una posición válida
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0,
                Random.Range(-spawnRadius, spawnRadius)
            );

            // Asegurarse de que la posición esté sobre el terreno y no en el agua
            if (IsValidSpawnPosition(randomPosition))
            {
                return randomPosition;
            }
        }

        return Vector3.zero; // Si no se encontró una posición válida
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        Ray ray = new Ray(new Vector3(position.x, 50, position.z), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            return true;
        }

        return false;
    }

    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
