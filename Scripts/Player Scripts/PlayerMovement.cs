using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private CharacterController characterController;
    private Vector3 moveDirection;
    public float speed = 5f;
    public float jumpForce = 10f;
    public float gravity = 25f;
    private float verticalVelocity;
    
    void Awake()
    {
        
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
     PlayerMove();
    }
    public void PlayerMove()
    {
        
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;
        ApplyGravity();
        characterController.Move(moveDirection);
    }
    public void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            PlayerJump();
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        moveDirection.y = verticalVelocity*Time.deltaTime;
    }
    public void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }
    }
}
