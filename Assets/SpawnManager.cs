using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerUp;
    public int enemyCount;
    public int waveNumber = 1;
    float spawnRange = 9;
    bool isBoss = false;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        var rd = Random.Range(0, powerUp.Length);
        Instantiate(powerUp[rd], GenerateSpawnPos(), powerUp[rd].transform.rotation);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
       
        if (enemiesToSpawn == 3) isBoss = true;
        else
        {
            isBoss = false;
        }
        if (isBoss)
        {
            Instantiate(enemyPrefab[2], GenerateSpawnPos(), enemyPrefab[2].transform.rotation);
        }
        else
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                var rd = Random.Range(0, 2);

                Instantiate(enemyPrefab[rd], GenerateSpawnPos(), enemyPrefab[rd].transform.rotation);
            }
        }
       

    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            var rd = Random.Range(0, powerUp.Length);
            Instantiate(powerUp[rd], GenerateSpawnPos(), powerUp[rd].transform.rotation);
        }
    }
}
