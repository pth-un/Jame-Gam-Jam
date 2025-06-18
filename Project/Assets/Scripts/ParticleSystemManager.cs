using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public static ParticleSystemManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SpawnParticles(GameObject particleSystem, Vector3 position, Quaternion rotation)
    {
        if (particleSystem)
        {
            GameObject particles = Instantiate(particleSystem);
            particles.transform.SetPositionAndRotation(position, rotation);
            particles.transform.SetParent(this.transform);
            foreach (ParticleSystem particle in particleSystem.GetComponentsInChildren<ParticleSystem>())
            {
                particle.Play(transform);
            }
        }
    }
}
