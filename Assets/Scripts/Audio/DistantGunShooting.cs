using System.Collections;
using UnityEngine;

public class DistantGunShooting : MonoBehaviour
{

    [SerializeField] AudioSource gunfireSource;
    [SerializeField] float minDelay = 2f;
    [SerializeField] float maxDelay = 15f;
    
    void Start()
    {
        StartCoroutine(PlayGunfireRandomly());
    }

    IEnumerator PlayGunfireRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);
            gunfireSource.Play();
        }
    }
}
