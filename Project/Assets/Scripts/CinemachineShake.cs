using System;
using Unity.Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineCamera cinemachineCamera;
    private float timer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    private void Start()
    {
        
        Gun.PlayerGun.OnGunShoot += Shake;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StopShake();
                timer = 0;
            }
        }
    }

    private void StopShake()
    {
        cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = 0;
    }

    private void Shake(object sender, Gun.OnGunShoot_Args e)
    {
        cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = e.intensity;
        timer = e.shakeTime;
    }
}
