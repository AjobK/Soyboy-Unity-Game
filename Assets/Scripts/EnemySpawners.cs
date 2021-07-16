using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawners : MonoBehaviour
{
    [Header("Spawn conditions")]
    [SerializeField] bool spawn = true;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    int spawnAmount = 1;

    [Header("Enemy prefabs")]
    [SerializeField] GameObject[] enemyPrefabs;

    [Header("Drops")]
    [SerializeField] GameObject[] items;

    GameObject[] enemySpawners;
    Coroutine spawnRoutine;
    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        if (gm) spawnAmount = spawnAmount + ((int) (gm.GetWaveNumber() * 1.2f));

        enemySpawners = GameObject.FindGameObjectsWithTag("enemySpawner");

        spawnRoutine = StartCoroutine(StartSpawning());
    }

    // Start is called before the first frame update
    IEnumerator StartSpawning()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();

            if (--spawnAmount <= 0) StopCoroutine(spawnRoutine);
        }
    }

    private void SpawnAttacker()
    {
        if (!spawn) return;
        GameObject newAttacker = Spawn();

        newAttacker.transform.parent = enemySpawners[0].transform;
    }

    private GameObject Spawn()
    {
        return Instantiate(
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
            enemySpawners[Random.Range(0, enemySpawners.Length)].transform.position,
            transform.rotation
        ) as GameObject;
    }

    // Update is called once per frame
    public void SetSpawn(bool spawn)
    {
        this.spawn = spawn;
    }

    public bool GetSpawning()
    {
        return spawn;
    }

    public int GetSpawnAmount()
    {
        return spawnAmount;
    }
}
