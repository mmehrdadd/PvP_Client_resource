using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;


    public void UpdateHealthBar(float health)
    {
        health /= 100f;
        healthBar.fillAmount = health;
    }
    public void UpdateStaminaBar(float stamina)
    {
        stamina /= 100f;
        staminaBar.fillAmount = stamina;
    }
}
