using System;
using TMPro;
using UnityEngine;

public class EnemiesMissedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyMissedTxt;

    private void Start()
    {
        GameManager.Instance.EnemyMissed_Event += OnEnemyMissed;
    }

    private void OnEnemyMissed(object sender, GameManager.EnemyMissed_EventArgs e)
    {
        enemyMissedTxt.text = "enemies missed- " + e.EnemiesMissed;
    }
}
