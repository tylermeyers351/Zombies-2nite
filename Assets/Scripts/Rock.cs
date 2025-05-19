// using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource boulderSmashAudioSource;
    [SerializeField] float shakeModifier = 10f;

    // CinemachineImpulseSource cinemachineImpulseSource;

    [SerializeField] float cooldownMax = 1f;
    [SerializeField] float cooldownTimer = 1f;


    void Awake()
    {
        // cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        fireImpulse();
        CollisionFX(other);
    }

    void fireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        // cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        // Debug.Log("Cooldown Timer: " + cooldownTimer);

        if (cooldownTimer > cooldownMax)
        {
            collisionParticleSystem.Play();
            boulderSmashAudioSource.Play();
            cooldownTimer = 0f;
        }
    }
}
