using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{
    [SerializeField] private float _sprintValue = 100f;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerAudio _playerAudio;
    //[SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _lookRoot;
    [SerializeField] private PlayerUI _playerStats;


    public void SetPosture(float newSpeed, float newHeight, float newSprintValue, float newStepDistance, float newMinSound, float newMaxSound)
    {

        _characterController.height = newHeight;
        _sprintValue = newSprintValue;
        _playerAudio.max_Sound = newMaxSound;
        _playerAudio.min_Sound = newMinSound;

    }
    public void UpdateUi(float value)
    {
        _playerStats.UpdateStaminaBar(value);
    }
}
