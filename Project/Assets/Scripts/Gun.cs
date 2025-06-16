using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private BulletProjectileSO bulletProjectileSO;
    [SerializeField] private Transform[] gunShootPositions;
    [SerializeField] private BulletsSpool bulletsSpool;

    private PlayerInputHandler playerInputHandler;
    private float shootTimer;
    private bool canShoot;

    private void Start()
    {
        playerInputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        HandleFire();
    }

    private void HandleFire()
    {
        CheckShoot();
        if (canShoot & playerInputHandler.CheckFiring())
        {
            foreach (Transform gunShootPos in gunShootPositions)
            {
                bulletsSpool.OnShoot(gunShootPos);
            }
            canShoot = false;
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
