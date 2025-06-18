using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidSpawnerSO", menuName = "Scriptable Objects/Asteroid/AsteroidSpawnerSO")]
public class AsteroidSpawnerSO : ScriptableObject
{
    public GameObject[] asteroidPrefabs;
    public float force;
    public float timeBetweenSpawn;
    public float forceMultiplier;
    public float timeMultiplier;
}
