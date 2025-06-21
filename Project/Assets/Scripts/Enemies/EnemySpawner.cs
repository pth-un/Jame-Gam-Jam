using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public event EventHandler onWaveDestroyed;

    [SerializeField]
    private List<GameObject> waves;

    [SerializeField] private PlayableDirector timelineAsset;
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private GameObject onBossFightScreen;

    private int currentWave = 0;
    private bool canSpawn = true;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        GameManager.Instance.SetWaveCount(waves.Count);
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

        if (!waves[currentWave].GetComponent<WaveScript>().isBossEnemy)
        {
            GameObject wave = Instantiate(waves[currentWave], spawnPosition);

            wave.GetComponent<WaveScript>().onWaveFinished += OnWaveFinished;
            currentWave++;
            wave.name = $"Wave_{currentWave}";
            canSpawn = false;

            UpdateWaveUI();
        }
        else
        {
            onBossFightScreen.SetActive(true);
            StartCoroutine(SpawnBossEnemy(2f));
        }
    }

    private void OnWaveFinished(object sender, EventArgs e)
    {
        onWaveDestroyed?.Invoke(this, EventArgs.Empty);
        canSpawn = true;
    }

    private IEnumerator Spawn()
    {
        timelineAsset.Pause();
        yield return new WaitUntil(() => canSpawn);
        SpawnWave();
    }

    private IEnumerator SpawnBossEnemy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        onBossFightScreen.SetActive(false);

        GameObject wave = Instantiate(waves[currentWave], spawnPosition);

        wave.GetComponent<WaveScript>().onWaveFinished += OnWaveFinished;
        wave.name = $"Wave_{currentWave++}";
        canSpawn = false;

        UpdateWaveUI(bossEnemy:true, bossEnemyNumber:wave.GetComponent<WaveScript>().bossFightNumber);
    }

    private void UpdateWaveUI(bool bossEnemy = false, int bossEnemyNumber=0)
    {
        if (!bossEnemy) wavesText.text = "wave number - " + currentWave.ToString();
        else wavesText.text = "boss fight number- " + bossEnemyNumber;
    } 
}
