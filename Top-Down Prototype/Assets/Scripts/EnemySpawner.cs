using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _enemyPrefab;
    float timeToNextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CanSpawn)
        {
            //rachel is the best person in the whole world
            int spawnChance = Random.Range(0, 101);
            if (spawnChance >= 0 && spawnChance <= 80)
            {
                var enemy = Instantiate(_enemyPrefab[0], transform.position, Quaternion.identity);
            }
            else if (spawnChance >= 81 && spawnChance <= 90)
            {
                var enemy = Instantiate(_enemyPrefab[3], transform.position, Quaternion.identity);
            }
            else if (spawnChance >= 91 && spawnChance <= 96)
            {
                var enemy = Instantiate(_enemyPrefab[1], transform.position, Quaternion.identity);
            }
            else if (spawnChance >= 97 && spawnChance <= 101)
            {
                var enemy = Instantiate(_enemyPrefab[2], transform.position, Quaternion.identity);
            }
            timeToNextSpawn = Time.time + 2f;
        }
    }

    private bool CanSpawn => Time.time >= timeToNextSpawn;
}
