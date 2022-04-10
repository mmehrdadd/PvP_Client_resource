using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource step_sound;
    private CharacterController CharacterController;
    [SerializeField]
    private AudioClip[] step_clip;
    [HideInInspector]
    public float min_Sound, max_Sound;

    private float accumuted_Distance;

    [HideInInspector]   
    public float step_Distance;
    void Awake()
    {
        step_sound = GetComponent<AudioSource>();
        CharacterController = GetComponentInParent<CharacterController>();
      
    }

    void Update()
    {
        CheckToPlaySound();
        
    }
    void CheckToPlaySound()
    {
     
        
    }
}
