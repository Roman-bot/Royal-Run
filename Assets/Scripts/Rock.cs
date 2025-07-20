using UnityEngine;
using Unity.Cinemachine;

public class Rock : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    CinemachineImpulseSource impulseSource;

    [SerializeField]
    ParticleSystem particleSystem; 

    AudioSource audioSource;

    float effectTimer; 

    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        effectTimer = 1f; // Initialize the effect timer
    }

    // Update is called once per frame
    void Update()
    {
        effectTimer = effectTimer + Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (effectTimer < 1f) return;
        FireImpilse();
        ColissionFX(collision);
        effectTimer = 0f; // Reset the timer after the effect is triggered
    }

    void FireImpilse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntencity = Mathf.Min(1f / distance, 1f); // Ensure the intensity does not exceed 1

        impulseSource.GenerateImpulse(shakeIntencity);
    }

    void ColissionFX(Collision other)
    {
        ContactPoint contact = other.GetContact(0);
        if (particleSystem != null)
        {
            particleSystem.transform.position = contact.point;
            particleSystem.Play();
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Particle system is not assigned or missing on the rock object.");
        }
    }
}
