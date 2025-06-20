using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float multiplier = 0.1f;
    [SerializeField] private float startIntensity = 0.5f;

    private float intensity = 0.8f;

    private void Start()
    {
        intensity = startIntensity;
        material.SetFloat("_Intensity", intensity);
    }

    private void Update()
    {
        if (material.GetFloat("_Intensity") > 0)
        {
            intensity -= multiplier * Time.deltaTime;
            material.SetFloat("_Intensity", Mathf.Clamp(intensity, 0, 1));
        }
    }
}
