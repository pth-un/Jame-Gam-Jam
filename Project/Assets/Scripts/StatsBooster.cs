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

    [SerializeField] private Transform statsUpdateParent;
    [SerializeField] private TextMeshProUGUI statsUpdate;

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
                UpdateStatsGUI(boostStat.statBoostedText);
                boostStat.boostStatsEvent?.Invoke(boostStat.boostStatValue);
            }
        }
    }

    private void UpdateStatsGUI(string _statBoostedText)
    {
        TextMeshProUGUI statsUpdateInst = Instantiate(statsUpdate, statsUpdateParent);
        statsUpdateInst.text = "Stats Upgraded -\n" + _statBoostedText;
        Destroy(statsUpdateInst.gameObject, 2.5f);
    }
}

