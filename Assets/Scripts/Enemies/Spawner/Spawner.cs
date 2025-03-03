using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    public GameObject enemyPrefab; // Prefab del enemigo a spawnear
    public Transform spawnPoint; // Punto de spawn
    public float spawnInterval = 3f; // Tiempo entre spawns
    public bool useSpawnLimit = false; // Activar o desactivar límite total de spawn
    public int maxTotalSpawns = 10; // Número total de enemigos a spawnear

    private int spawnedEnemies = 0; // Contador de enemigos generados

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Si está activado el límite y ya alcanzó el máximo, detener el spawn
            if (useSpawnLimit && spawnedEnemies >= maxTotalSpawns)
            {
                yield break; // Detiene la corrutina y no genera más enemigos
            }

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnedEnemies++; // Aumenta el contador de enemigos generados
    }
}

