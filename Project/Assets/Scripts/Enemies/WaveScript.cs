using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    public event EventHandler onWaveFinished;

    public bool isBossEnemy;
    public int bossFightNumber;

    private List<Enemy> listOfEnemies;

    private void Start()
    {
        listOfEnemies = new List<Enemy>();
        listOfEnemies = GetComponentsInChildren<Enemy>().ToList<Enemy>();
    }

    public void OnEnemyDie(Enemy enemy)
    {
        listOfEnemies.Remove(enemy);
        if (listOfEnemies.Count == 0)
        {
            onWaveFinished?.Invoke(this, EventArgs.Empty);
            Destroy(this.gameObject);
        }
    }
}
