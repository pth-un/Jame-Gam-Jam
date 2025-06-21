using UnityEngine;

public class SineWaveEnemy : Enemy
{
    [Header("SineWaveSettings")]
    [SerializeField] private float amplitude = 5f;
    [SerializeField] private float frequency = 0.4f;
    [SerializeField] private bool inverted;

    private float sinCenterX;

    private void Start()
    {
        SoundManager.Instance.PlaySoundClipFromArray(enemySpawnClips, transform.position);
        sinCenterX = transform.position.x;
    }

    private void Update()
    {
        HandleShootAllow();
        HandleMoveAllow();
        Move();
        Debug.Log(shootingAllowed);
        if (shootingAllowed) Shoot();
    }

    protected override void Shoot()
    {
        GetComponent<Gun>().HandleFire(1 << gameObject.layer, shootTowardsPlayer);
    }

    protected override void Move()
    {
        if (movementAllow)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            Vector3 move = transform.position;

            float sin = Mathf.Sin(move.z * frequency) * amplitude;
            if (inverted) sin *= -1;
            move.x = sinCenterX + sin;

            transform.position = move;
        }
    }
}
