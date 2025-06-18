using System;
using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private float force;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private float forceMultiplier;
    [SerializeField] private float timeMultiplier;

    private bool coroutineStarted = false;
    private void Update()
    {
        if(!coroutineStarted) StartCoroutine(WaitCoroutine(timeBetweenSpawn));
    }

    private IEnumerator WaitCoroutine(float waitTime)
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(waitTime);
        SpawnAsteroids();
        force *= forceMultiplier;
        timeBetweenSpawn *= timeMultiplier;
        coroutineStarted = false;
    }

    private void SpawnAsteroids()
    {
        Transform spawnLocation = spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Length - 1)];
        GameObject asteroidPrefab = asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Length - 1)];

        GameObject instance = Instantiate(asteroidPrefab, spawnLocation);
        instance.GetComponent<AsteroidMovement>().OnSpawn(force);
        //instance.GetComponent<Rigidbody>().AddForce(instance.transform.forward * 100, ForceMode.Impulse);
        //asteroidPrefab.transform.SetParent(this.transform);
    }
}
