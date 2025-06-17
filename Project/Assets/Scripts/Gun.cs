using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private BulletProjectileSO bulletProjectileSO;
    [SerializeField] private Transform[] gunShootPositions;
    [SerializeField] private BulletsSpool bulletsSpool;
    [SerializeField] private bool isPlayerGun;

    private PlayerInputHandler playerInputHandler;
    private float shootTimer;
    private bool canShoot;

    private void Start()
    {
        if (isPlayerGun) playerInputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        if (isPlayerGun) HandleFire();
    }

    public void HandleFire(bool towardsPlayer=false)
    {
        CheckShoot();
        if (isPlayerGun)
        {
            if (canShoot && playerInputHandler.CheckFiring())
            {
                foreach (Transform gunShootPos in gunShootPositions)
                {
                    bulletsSpool.OnShoot(gunShootPos);
                }
                canShoot = false;
            }
        }
        else
        {
            if (canShoot)
            {
                foreach (Transform gunShootPos in gunShootPositions)
                {
                    bulletsSpool.OnShoot(gunShootPos, towardsPlayer);
                }
                canShoot = false;
            }
        }
    }


    private void CheckShoot()
    {
        if (!canShoot)
        {
            shootTimer += Time.deltaTime;
            float fireRate = 60 / bulletProjectileSO.fireRate;
            if (shootTimer >= fireRate)
            {
                canShoot = true;
                shootTimer = 0f;
            }
        }
    }
}
