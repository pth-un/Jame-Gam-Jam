using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    public event EventHandler onWaveFinished;

    public bool isBossEnemy;
    public int bossFightNumber;
    public float cameraShakeIntensity = 0.4f;

    private List<Enemy> listOfEnemies;

    private void Start()
    {
        listOfEnemies = new List<Enemy>();
        listOfEnemies = GetComponentsInChildren<Enemy>().ToList<Enemy>();
        CinemachineShake.Instance.SetCameraShake(cameraShakeIntensity);
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
