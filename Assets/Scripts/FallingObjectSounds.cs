using UnityEngine;

public class FallingObjectSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] thwackSounds;

    [SerializeField] float cooldownMax = 1f;
    [SerializeField] float cooldownTimer = 1f;

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        CollisionFX(other);
    }

    void CollisionFX(Collision other)
    {
        if (cooldownTimer > cooldownMax)
        {
            PlayThwack();
            cooldownTimer = 0f;
        }
    }
    
    void PlayThwack()
    {
        int randomIndex = Random.Range(0, thwackSounds.Length);
        audioSource.clip = thwackSounds[randomIndex];
        audioSource.Play();
    }
}
