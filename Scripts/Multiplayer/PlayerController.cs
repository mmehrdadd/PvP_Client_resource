using RiptideNetworking;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
    [SerializeField] private PlayerUI playerUI;
    private float stamina, staminaThreashold;

    private bool[] inputs;
    private void Start()
    {
        stamina = 100f;
        staminaThreashold = 10f;
        inputs = new bool[10];
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            inputs[0] = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputs[1] = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputs[2] = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputs[3] = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina >= 0)
            {
                inputs[4] = true;
                StaminaDecreasing();
            }            
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            inputs[5] = true;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            inputs[6] = true;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            inputs[7] = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            inputs[8] = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            inputs[9] = true;
           
        }
    }

    private void StaminaDecreasing()
    {
        if(stamina >= 0)
        {
            stamina -= Time.deltaTime * staminaThreashold;
            playerUI.UpdateStaminaBar(stamina);
        }
        
    }

    private void StaminaRegenaration()
    {
        if(stamina != 100f)
        {
            stamina += Time.deltaTime / 3f;
            playerUI.UpdateStaminaBar(stamina);
        }
        if(stamina > 100f)
        {
            stamina = 100f;
            playerUI.UpdateStaminaBar(stamina);
        }
    }

    private void FixedUpdate()
    {        
        SendInput();
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i] = false;
        }
        StaminaRegenaration();
    }

    private void SendInput()
    {
        Message message = Message.Create(MessageSendMode.unreliable, ClientToServerId.input);
        message.AddBools(inputs, false);
        message.AddVector3(camTransform.forward);
        NetworkManager.instance.client.Send(message);
    }
}
