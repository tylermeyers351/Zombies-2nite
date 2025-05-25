using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepAudio : MonoBehaviour
{
    public StarterAssets.StarterAssetsInputs input;
    public AudioClip[] footstepClips;
    [SerializeField] float footstepInterval = 0.5f; // Adjust for walking pace

    [SerializeField] AudioSource footstepAudioSource;
    private float stepTimer;

    void Start()
    {
        if (input == null)
        {
            input = GetComponent<StarterAssets.StarterAssetsInputs>();
        }
    }

    void Update()
    {
        if (input == null || input.move.magnitude < 0.1f)
        {
            stepTimer = 0f;
            return;
        }

        stepTimer -= Time.deltaTime;

        if (stepTimer <= 0f)
        {
            PlayFootstep();
            stepTimer = footstepInterval;
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        footstepAudioSource.pitch = Random.Range(0.95f, 1.05f);
        int index = Random.Range(0, footstepClips.Length);
        footstepAudioSource.PlayOneShot(footstepClips[index]);
    }
}
