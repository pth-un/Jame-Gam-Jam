using UnityEngine;

public class SineWaveEnemy : Enemy
{
    [Header("SineWaveSettings")]
    [SerializeField] private float amplitude = 2f;
    [SerializeField] private float frequency = 0.4f;
    [SerializeField] private bool inverted, shootConstantly;

    private float sinCenterX;

    private void Start()
    {
        sinCenterX = transform.position.x;
    }

    private void Update()
    {
        Move();
    }

    protected override void Shoot()
    {
        GetComponent<Gun>().HandleFire(shootTowardsPlayer);
    }

    protected override void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        Vector3 move = transform.position;

        float sin = Mathf.Sin(move.z * frequency) * amplitude;
        if (inverted) sin *= -1;
        move.x = sinCenterX + sin;

        transform.position = move;
        if(shootConstantly) Shoot();
    }
}
