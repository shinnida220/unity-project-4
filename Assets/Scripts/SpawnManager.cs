using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9.0f;

    public int enemyCount;
    public int waveCount;

    public GameObject powerupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ++waveCount;
        SpawnEnemyWave(waveCount);
        GeneratePowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount < 1) {
            ++waveCount;
            SpawnEnemyWave(waveCount);
            // Generate new power up
            GeneratePowerUp();
        }
    }

    private Vector3 GenerateSpawnPostion()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX,0,spawnPosZ);
    }

    private void GeneratePowerUp() 
    {
        Instantiate(powerupPrefab, GenerateSpawnPostion(), powerupPrefab.transform.rotation);
    }

    private void SpawnEnemyWave (int enemiesToSpawn)
    {
        for (int j=0; j<enemiesToSpawn; j++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPostion(), enemyPrefab.transform.rotation);
        }
    }
}
