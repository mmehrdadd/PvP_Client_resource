using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWooshSound : MonoBehaviour
{

    [SerializeField] private AudioClip[] wooshSounds;
    [SerializeField] private AudioSource audioSource;
    
    
    void WooshSound()
    {
        audioSource.clip = wooshSounds[Random.Range(0, wooshSounds.Length)];
        audioSource.Play();
    }
}
