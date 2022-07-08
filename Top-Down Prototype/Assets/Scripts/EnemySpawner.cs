using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 0f;
    [SerializeField] private bool isLooping;
    private WaveConfig currentWave;


    public WaveConfig CurrentWave => currentWave;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(SpawnEnemyWaves());
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private IEnumerator SpawnEnemyWaves()
    {

        foreach (WaveConfig wave in waveConfigs)
        {
            currentWave = wave;
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                _ = Instantiate(currentWave.GetEnemyPrefab(i),
                    transform.position, Quaternion.identity, transform);
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }

    }
}
