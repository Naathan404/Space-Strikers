using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;
    [SerializeField] private int waveIndex = 0;
    private float timer = 0;
    private PlayerController playerController;

    [System.Serializable]
    public class Wave
    {
        public ObjectPooler objectPooler;
        public float spawnInterval;
        public int objectPerWave;
        public int objectCount;
        public float minSpawnPos;
        public float maxSpawnPos;
    }

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        timer += Time.deltaTime * playerController.GetBoost();
        if (timer >= waves[waveIndex].spawnInterval)
        {
            timer = 0f;
            SpawnObject();
            waves[waveIndex].objectCount++;
        }
        if (waves[waveIndex].objectCount >= waves[waveIndex].objectPerWave)
        {
            waves[waveIndex].objectCount = 0;
            waveIndex++;
            if (waveIndex >= waves.Count)
            {
                waveIndex = 0;
            }
        }
    }

    private void SpawnObject()
    {
        GameObject pooledObject = waves[waveIndex].objectPooler.GetPooledObject();
        pooledObject.transform.position = new Vector2(10f, Random.Range(waves[waveIndex].minSpawnPos, waves[waveIndex].maxSpawnPos));
        pooledObject.SetActive(true);
    }
}
