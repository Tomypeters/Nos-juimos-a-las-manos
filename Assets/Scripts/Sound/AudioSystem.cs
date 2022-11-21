using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip whoosh;
    public AudioClip heavyWhoosh;
    public AudioClip normalHit;
    public AudioClip ouch;
    public AudioClip dash;
    public AudioClip enemyDeath;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWhoosh() 
    {
        if (whoosh != null) audioSource.PlayOneShot(whoosh);
    }

    public void PlayHeavyWhoosh()
    {
        if (heavyWhoosh != null) audioSource.PlayOneShot(heavyWhoosh);
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


    public void PlayEnemyDeath()
    {
        if (enemyDeath != null) audioSource.PlayOneShot(enemyDeath);

    }
}
