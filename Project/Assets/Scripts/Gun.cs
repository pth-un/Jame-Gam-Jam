using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun PlayerGun;

    public event EventHandler<OnGunShoot_Args> OnGunShoot;
    public class OnGunShoot_Args : EventArgs { public float intensity; public float shakeTime; }

    [SerializeField] private float bulletsFireRate;
    [SerializeField] private Transform[] gunShootPositions;
    [SerializeField] private AudioClip[] gunShootAudionClips;
    [SerializeField] private bool isPlayerGun;
    [SerializeField] private float shakeIntensity, shakeTime;


    private BulletsSpool bulletsSpool;
    private PlayerInputHandler playerInputHandler;
    private float shootTimer;
    private bool canShoot;
    private float fireRate;

    private void Awake()
    {
        if (PlayerGun == null) PlayerGun = this;
    }

    private void Start()
    {
        fireRate = 60 / bulletsFireRate;
        if (isPlayerGun)
        {
            playerInputHandler = PlayerInputHandler.Instance;
            if (PlayerGun == null) PlayerGun = this;
        }
        bulletsSpool = BulletsSpool.Instance;
    }

    private void Update()
    {
        if (isPlayerGun)
        {
            HandleFire(1 << gameObject.layer);
        }
    }

    public void HandleFire(LayerMask layerMask, bool towardsPlayer=false)
    {
        if (!bulletsSpool) return;
        CheckShoot();
        if (isPlayerGun)
        {
            if (canShoot && playerInputHandler.CheckFiring())
            {
                foreach (Transform gunShootPos in gunShootPositions)
                {
                    bulletsSpool.OnShoot(gunShootPos, layerMask);
                    PlayShootSound(gunShootPos);
                    OnGunShoot?.Invoke(this, new OnGunShoot_Args { intensity = shakeIntensity, shakeTime = shakeTime});
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
                    bulletsSpool.OnShoot(gunShootPos, layerMask, towardsPlayer);
                    PlayShootSound(gunShootPos);
                }
                canShoot = false;
            }
        }
    }

    private void PlayShootSound(Transform soundPosition)
    {
        SoundManager.Instance.PlaySoundClipFromArray(gunShootAudionClips, soundPosition.position);
    }


    private void CheckShoot()
    {
        if (!canShoot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= fireRate)
            {
                canShoot = true;
                shootTimer = 0f;
            }
        }
    }
}
