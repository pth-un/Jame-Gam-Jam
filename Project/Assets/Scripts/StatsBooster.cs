using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoostStatEvent : UnityEvent<float> { }


[System.Serializable]
public class BoostStats
{
    public int boostStatsAfterWave;
    public BoostStatEvent boostStatsEvent;
    public float boostStatValue;
    public string statBoostedText;
}


public class StatsBooster : MonoBehaviour
{
    public BoostStats[] boostStats;

    

    private void Start()
    {
        GameManager.Instance.BoostStats += HandleBoostStats;
    }

    private void HandleBoostStats(object sender, GameManager.HandleBoostStats_EventArgs e)
    {
        foreach (BoostStats boostStat in boostStats)
        {
            if (boostStat.boostStatsAfterWave == e.WavesDestroyed)
            {
                GameManager.Instance.UpdateStatsGUI("stats upgraded- \n" + boostStat.statBoostedText);
                boostStat.boostStatsEvent?.Invoke(boostStat.boostStatValue);
            }
        }
    }
}

