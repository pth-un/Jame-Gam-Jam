using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler<HandleBoostStats_EventArgs> BoostStats;
    public class HandleBoostStats_EventArgs : EventArgs
    {
        public int WavesDestroyed { get; set; }
    }

    public event EventHandler<EnemyMissed_EventArgs> EnemyMissed_Event;
    public class EnemyMissed_EventArgs : EventArgs
    {
        public int EnemiesMissed { get; set; }
    }

    [SerializeField] private Transform statsUpdateParent;
    [SerializeField] private TextMeshProUGUI statsUpdateTxt;
    

    private int enemiesMissed = 0;
    private int wavesDestroyed = 0;
    private int currentWave = 1;
    private int wavesLeft;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        EnemySpawner.Instance.onWaveDestroyed += WaveDestroyed;
        EnemyMissed_Event?.Invoke(this, new EnemyMissed_EventArgs { EnemiesMissed = 0 });
    }

    private void WaveDestroyed(object sender, EventArgs e)
    {
        wavesLeft--;
        currentWave++;
        wavesDestroyed++;
        BoostStats?.Invoke(this, new HandleBoostStats_EventArgs { WavesDestroyed = wavesDestroyed });
    }

    public void SetWaveCount(int _waveCount)
    {
        wavesLeft = _waveCount;
    }

    public void EnemyMissed()
    {
        enemiesMissed++;
        EnemyMissed_Event?.Invoke(this, new EnemyMissed_EventArgs { EnemiesMissed = enemiesMissed });
    }

    public void BossEnemyDestroyed()
    {
        PlayerHealth.Instance.ResetPlayerHealth();
        UpdateStatsGUI("Boss Enemy Destroyed");
    }

    public void UpdateStatsGUI(string updateNotifTxt)
    {
        TextMeshProUGUI statsUpdateInst = Instantiate(statsUpdateTxt, statsUpdateParent);
        statsUpdateInst.text = updateNotifTxt;
        Destroy(statsUpdateInst.gameObject, 2.5f);
    }
}