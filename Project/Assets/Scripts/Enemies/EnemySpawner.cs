using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> waves;

    [SerializeField] private PlayableDirector timelineAsset;

    private int currentWave = 0;
    private bool canSpawn=true;

    [SerializeField]
    private Transform spawnPosition;

    private void Start()
    {
        Debug.Log(waves);
    }

    public void CueSpawnWave()
    {

        if (currentWave < waves.Count)
        {
            StartCoroutine(Spawn());
        }
    }

    private void SpawnWave()
    {
        timelineAsset.Resume();
        
        GameObject wave = Instantiate(waves[currentWave], spawnPosition);
        wave.GetComponent<WaveScript>().onWaveFinished += OnWaveFinished;
        wave.name = $"Wave_{currentWave++}";
        canSpawn = false;
    }

    private void OnWaveFinished(object sender, EventArgs e)
    {
        canSpawn = true;
    }

    private IEnumerator Spawn()
    {
        timelineAsset.Pause();
        yield return new WaitUntil(() => canSpawn);
        SpawnWave();
    }
}
