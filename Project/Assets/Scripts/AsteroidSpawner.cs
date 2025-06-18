using System;
using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private AsteroidSpawnerSO[] asteroidSpawnerSOs;

    private bool coroutineStarted = false;
    private AsteroidSpawnerSO asteroidSpawnerSO;

    private void Start()
    {
        asteroidSpawnerSO = asteroidSpawnerSOs[0];
    }
    private void Update()
    {
        if (!coroutineStarted) StartCoroutine(WaitCoroutine(asteroidSpawnerSO.timeBetweenSpawn));
    }

    private IEnumerator WaitCoroutine(float waitTime)
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(waitTime);
        SpawnAsteroids();
        asteroidSpawnerSO.force *= asteroidSpawnerSO.forceMultiplier;
        asteroidSpawnerSO.timeBetweenSpawn *= asteroidSpawnerSO.timeMultiplier;
        coroutineStarted = false;
    }

    private void SpawnAsteroids()
    {
        Transform spawnLocation = spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Length - 1)];
        GameObject asteroidPrefab = asteroidSpawnerSO.asteroidPrefabs[UnityEngine.Random.Range(0, asteroidSpawnerSO.asteroidPrefabs.Length - 1)];

        GameObject instance = Instantiate(asteroidPrefab, spawnLocation);
        instance.GetComponent<AsteroidMovement>().OnSpawn(asteroidSpawnerSO.force);
        //instance.GetComponent<Rigidbody>().AddForce(instance.transform.forward * 100, ForceMode.Impulse);
        //asteroidPrefab.transform.SetParent(this.transform);
    }
}
