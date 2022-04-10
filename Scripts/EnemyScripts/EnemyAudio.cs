using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] enemyAttackSounds;

    [SerializeField]
    private AudioClip screamSound, dieSound;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void ScreamSound()
    {
        audioSource.clip = screamSound;
        audioSource.Play();
    }

    public void DieSound()
    {
        audioSource.clip = dieSound;
        audioSource.Play();
    }

    public void EnemyAttackSounds()
    {
        audioSource.clip = enemyAttackSounds[Random.RandomRange(0, enemyAttackSounds.Length)];
        audioSource.Play();
    }
    
}
