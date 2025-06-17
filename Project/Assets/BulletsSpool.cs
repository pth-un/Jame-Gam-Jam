using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletsSpool : MonoBehaviour
{
    [SerializeField] private int spawnBulletCount;
    [SerializeField] private BulletProjectileSO bulletProjectileSO;

    private List<GameObject> bulletProjectiles;
    private int disabledBullets;

    private void Awake()
    {
        bulletProjectiles = new List<GameObject>();
    }

    private void Start()
    {
        SpawnBullets();
    }

    public void OnShoot(Transform gunShootPos, bool towardsPlayer=false)
    {
        Debug.Log(GetInactiveBulletProjectile());
        GameObject bulletProjectile_InstToFire = GetInactiveBulletProjectile();


        bulletProjectile_InstToFire.SetActive(true);
        bulletProjectile_InstToFire.transform.SetPositionAndRotation(position: gunShootPos.position, rotation: gunShootPos.rotation);
        if (towardsPlayer)
        {
            bulletProjectile_InstToFire.transform.LookAt(PlayerInputHandler.Instance.transform);
        }
        bulletProjectile_InstToFire.GetComponent<Rigidbody>().AddForce(bulletProjectile_InstToFire.transform.forward * bulletProjectileSO.bulletProjectileForce, ForceMode.Impulse);
        bulletProjectile_InstToFire.GetComponent<Projectile>().OnSpawn();
    }

    private GameObject GetInactiveBulletProjectile()
    {
        foreach (GameObject bulletProjectile in bulletProjectiles)
        {
            if (!bulletProjectile.activeSelf)
            {
                return bulletProjectile;
            }
        }
        SpawnBullets();
        return bulletProjectiles[bulletProjectiles.Count - 1];
    }

    public void ResetBullet(GameObject bulletToReset)
    {
        bulletToReset.SetActive(false);
        bulletToReset.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        bulletToReset.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        disabledBullets++;

        if (disabledBullets >= spawnBulletCount)
        {
            bulletProjectiles.Remove(bulletToReset);
            Destroy(bulletToReset);
        }
    }

    private void SpawnBullets()
    {
        for (int i = 0; i <= spawnBulletCount; i++)
        {
            GameObject bulletProjectile_Inst = Instantiate(bulletProjectileSO.visual, this.transform);
            bulletProjectile_Inst.SetActive(false);
            bulletProjectiles.Add(bulletProjectile_Inst);
            
            disabledBullets++;
        }
    }
}
