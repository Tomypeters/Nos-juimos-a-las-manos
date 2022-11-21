using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip whoosh;
    public AudioClip normalHit;
    public AudioClip ouch;
    public AudioClip dash;

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
        if(ouch != null) audioSource.PlayOneShot(ouch);
    }

    public void PlayStunned()
    {
        // sonido de estoy aturdido.
    }

    public void PlayBlocked() 
    { 
        //sonido de blockie.
    }

    public void PlayDash() 
    { 
        if(dash != null) audioSource.PlayOneShot(dash);

    }


}
