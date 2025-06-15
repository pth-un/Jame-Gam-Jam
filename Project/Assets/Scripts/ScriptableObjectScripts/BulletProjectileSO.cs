using UnityEngine;

[CreateAssetMenu(fileName = "BulletProjectileSO", menuName = "Scriptable Objects/BulletProjectileSO")]
public class BulletProjectileSO : ScriptableObject
{
    public float bulletProjectileForce;
    public float damage;
    public float fireRate;
    public GameObject visual;
}
