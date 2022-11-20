using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip whoosh;
    public AudioClip normalHit;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWhoosh() 
    {
        if (whoosh != null) audioSource.PlayOneShot(whoosh);
    }

    public void PlayHitEnemy() 
    {
        if(normalHit != null) audioSource.PlayOneShot(normalHit);
    }

    public void PlayTakeDamage() 
    { 
        // sonido de me pegaron.
    }

    public void PlayStunned()
    {
        // sonido de estoy aturdido.
    }

    public void PlayBlocked() 
    { 
        //sonido de blockie.
    }


}
