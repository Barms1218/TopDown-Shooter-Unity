using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Enemy Waves")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private float waveSpawnDelay = 3f;
    [SerializeField] private float spawnTimeVariation = 0.5f;
    [SerializeField] private float minSpawnTime = 0.5f;

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(waveSpawnDelay - spawnTimeVariation, waveSpawnDelay + spawnTimeVariation);

        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
    }

    public int GetEnemyCount() => enemyPrefabs.Count;
    public GameObject GetEnemyPrefab(int index) => enemyPrefabs[index];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
