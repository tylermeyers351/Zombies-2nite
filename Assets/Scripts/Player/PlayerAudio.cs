using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource reloadAudio;
    [SerializeField] AudioSource hurtPlayerAudioSource;
    [SerializeField] AudioClip[] hurtPlayerAudioClips;

    public void playReloadAudio()
    {
        reloadAudio.Play();
    }

    public void playHurtPlayerAudio()
    {
        int randomClip = Random.Range(0, hurtPlayerAudioClips.Length);
        hurtPlayerAudioSource.clip = hurtPlayerAudioClips[randomClip];
        hurtPlayerAudioSource.Play();
    }
}
