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
}