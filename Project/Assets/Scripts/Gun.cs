using System;
using System.Collections;
using TMPro;
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
    [SerializeField] private GameObject bulletsLeftUI;
    [SerializeField] private AudioClip[] reloadSoundClips;
    [SerializeField] private float playerMagazineMax = 30f, reloadTime;
    [SerializeField] private float shakeIntensity, shakeTime;
    [SerializeField] private BulletsSpool bulletsSpool;


    private PlayerInputHandler playerInputHandler;
    private float shootTimer;
    private float playerMagazine;
    private bool canShoot;
    private float fireRate;
    private bool reloading;

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
            playerInputHandler.OnReloadEvent += ReloadFromInput;
            if (PlayerGun == null) PlayerGun = this;
            playerMagazine = playerMagazineMax;
            bulletsLeftUI.GetComponentInChildren<TextMeshProUGUI>().text = playerMagazineMax + " / " + playerMagazineMax;

        }
    }

    private void ReloadFromInput(object sender, EventArgs e)
    {
        StartReload();
    }

    private void Update()
    {
        if (isPlayerGun)
        {
            HandleFire(1 << gameObject.layer);
        }
    }

    public void HandleFire(LayerMask layerMask, bool towardsPlayer = false)
    {
        if (reloading) return;
        if (!bulletsSpool) return;
        CheckShoot();
        if (isPlayerGun)
        {
            if (canShoot && playerInputHandler.CheckFiring())
            {
                foreach (Transform gunShootPos in gunShootPositions)
                {
                    playerMagazine--;
                    if (playerMagazine > 0)
                    {
                        bulletsSpool.OnShoot(gunShootPos, layerMask);
                        PlayShootSound(gunShootPos);
                        bulletsLeftUI.GetComponentInChildren<TextMeshProUGUI>().text = playerMagazine + " / " + playerMagazineMax;
                        OnGunShoot?.Invoke(this, new OnGunShoot_Args { intensity = shakeIntensity, shakeTime = shakeTime });
                    }
                    else
                    {
                        StartReload();
                    }
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

    private void StartReload()
    {
        SoundManager.Instance.PlaySoundClipFromArray(reloadSoundClips, transform.position);
        bulletsLeftUI.GetComponent<AnimateBulletIcon>().Rotate(reloadTime);
        bulletsLeftUI.GetComponentInChildren<TextMeshProUGUI>().text = "- / " + playerMagazineMax;
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
        playerMagazine = playerMagazineMax;
        bulletsLeftUI.GetComponentInChildren<TextMeshProUGUI>().text = playerMagazine + " / " + playerMagazineMax;
    }

    public void AddMagazine(int addMagSize)
    {
        playerMagazineMax += addMagSize;
        bulletsLeftUI.GetComponentInChildren<TextMeshProUGUI>().text = playerMagazine + " / " + playerMagazineMax;
    }

    public void AddFireRate(int addFireRate)
    {
        bulletsFireRate += addFireRate;
    }

    public void ReduceReloadTime(float reduceReloadTime)
    {
        reloadTime -= reduceReloadTime;
    }
}
