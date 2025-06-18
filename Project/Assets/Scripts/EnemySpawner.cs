using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> waves;

    private int currentWave = 0;

    [SerializeField]
    private Transform spawnPosition;
    public void SpawnWave() 
    {
        Debug.Log("Wave " + currentWave);

        if (currentWave < waves.Count)
        {
            GameObject wave = Instantiate(waves[currentWave], spawnPosition);

            wave.name = $"Wave_{currentWave++}";
        }
    }
}
