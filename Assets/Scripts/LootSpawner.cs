using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] GameObject keyPrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float coinDropChance = 25f; // In percentages

    // Cached reference
    EnemySpawners enemySpawners;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawners = FindObjectOfType<EnemySpawners>();
    }

    public void SpawnLoot()
    {
        if (enemySpawners.GetSpawnAmount() <= 0 && FindObjectsOfType<LootSpawner>().Length <= 1)
        {
            SpawnKey();
        }
        else
        {
            SpawnCoin();
        }
    }


    // Update is called once per frame
    void SpawnKey()
    {
        GameObject key = Instantiate(
            keyPrefab,
            gameObject.transform.position,
            Quaternion.identity
        ) as GameObject;
    }

    void SpawnCoin()
    {
        if (Random.Range(1, 100) <= coinDropChance)
        {
            GameObject coin = Instantiate(
               coinPrefab,
               gameObject.transform.position,
               Quaternion.identity
           ) as GameObject;
        }
    }
}
