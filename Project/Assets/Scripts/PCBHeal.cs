using UnityEngine;

public class PCBHeal : MonoBehaviour
{
    [SerializeField] private float force=70f;
    [SerializeField] private float heal;

    private Rigidbody rb;

    private void Start()
    {
        Debug.Log("Here");
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            playerHealth.Heal(heal);
            Destroy(this.gameObject);
        }
    }
}
