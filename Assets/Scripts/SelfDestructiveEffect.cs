using UnityEngine;

public class SelfDestructiveEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ParticleSystem parts = GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetime.constantMax;
        Destroy(gameObject, totalDuration);
    }
}
