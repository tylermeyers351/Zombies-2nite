using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource reloadAudio;

    public void playReloadAudio()
    {
        reloadAudio.Play();
    }
}
