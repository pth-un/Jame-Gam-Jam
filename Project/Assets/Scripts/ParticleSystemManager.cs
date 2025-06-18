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

    public void SpawnParticles(GameObject particleSystem, Transform transform)
    {
        if (particleSystem)
        {
            GameObject particles = Instantiate(particleSystem);
            particles.transform.SetPositionAndRotation(transform.position, transform.rotation);
            particles.transform.SetParent(this.transform);
            foreach (ParticleSystem particle in particleSystem.GetComponentsInChildren<ParticleSystem>())
            {
                Debug.Log("Here");
                particle.Play(transform);
            }
        }
    }
}
