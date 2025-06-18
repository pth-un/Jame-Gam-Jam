using System;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] private float rotationForce;
    [SerializeField] private float damageToPlayer = 30f;

    private Rigidbody rb;
    private void ChangeRotation()
    {
        Vector3 randomTorque = UnityEngine.Random.onUnitSphere * rotationForce;
        rb.AddTorque(randomTorque, ForceMode.Impulse);
    }

    public void OnSpawn(float force)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x + 1, transform.rotation.eulerAngles.y + 1, transform.rotation.eulerAngles.z + 1));
        ChangeRotation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerHealth>())
        {
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer);
            Destroy(this.gameObject);
        }
    }
}
