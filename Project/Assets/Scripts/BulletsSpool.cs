using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BulletsSpool : MonoBehaviour
{
    [SerializeField] private int spawnBulletCount;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private bool isPlayerSpool;


    private List<GameObject> bulletProjectiles;
    private int disabledBullets;

    private void Awake()
    {
        bulletProjectiles = new List<GameObject>();
    }

    private void Start()
    {
        if(isPlayerSpool) enemySpawner.onWaveDestroyed += ResetAllBullets;
        SpawnBullets();
    }

    public void OnShoot(Transform gunShootPos, LayerMask layerMask, bool towardsPlayer = false)
    {
        GameObject bulletProjectile_InstToFire = GetInactiveBulletProjectile();
        bulletProjectile_InstToFire.GetComponent<Rigidbody>().excludeLayers += layerMask;

        bulletProjectile_InstToFire.SetActive(true);
        bulletProjectile_InstToFire.transform.SetPositionAndRotation(position: gunShootPos.position, rotation: gunShootPos.rotation);
        if (towardsPlayer)
        {
            bulletProjectile_InstToFire.transform.LookAt(PlayerInputHandler.Instance.transform);
        }
        bulletProjectile_InstToFire.GetComponent<Projectile>().OnSpawn();
    }

    private GameObject GetInactiveBulletProjectile()
    {
        foreach (GameObject bulletProjectile in bulletProjectiles)
        {
            if (bulletProjectile && !bulletProjectile.activeSelf)
            {
                return bulletProjectile;
            }
        }
        SpawnBullets();
        foreach (GameObject bulletProjectile in bulletProjectiles)
        {
            if (!bulletProjectile.activeSelf)
            {
                return bulletProjectile;
            }
        }
        return null;
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
            GameObject bulletProjectile_Inst = Instantiate(projectilePrefab, this.transform);
            bulletProjectile_Inst.SetActive(false);
            bulletProjectiles.Add(bulletProjectile_Inst);
            
            disabledBullets++;
        }
    }

    private void ResetAllBullets(object sender, EventArgs e)
    {
        var activeBullets = bulletProjectiles.Where(bullet => bullet.activeSelf).ToList();
        foreach (GameObject bullet in activeBullets)
        {
            ResetBullet(bullet);
        }
    }

}
