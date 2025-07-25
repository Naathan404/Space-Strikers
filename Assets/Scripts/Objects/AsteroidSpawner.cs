using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    private PlayerController playerController;
    private ObjectPooler asteroidPool;
    private float timer = 0f;

    private void Awake()
    {
        asteroidPool = GetComponent<ObjectPooler>();
        playerController = FindAnyObjectByType<PlayerController>();
    }
    
    private void Update()
    {
        timer += Time.deltaTime * playerController.GetBoost();
        if (timer > spawnInterval)
        {
            SpawnAsteroid();
            timer = 0f;
        }

    }

    private void SpawnAsteroid()
    {
        GameObject asteroid = asteroidPool.GetPooledObject();
        asteroid.transform.position = new Vector2(10f, Random.Range(-5f, 5f));
        asteroid.SetActive(true);
    }
}
