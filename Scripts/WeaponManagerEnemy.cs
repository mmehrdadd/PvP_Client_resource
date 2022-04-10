using UnityEngine;
using RiptideNetworking;
public class WeaponManagerEnemy : MonoBehaviour
{
    [SerializeField] private WeaponHandler[] weapons;
    public int currentWeapon;

    void Start()
    {
        currentWeapon = 0;
        weapons[currentWeapon].gameObject.SetActive(true);
    }
    public void TurnOnSelectedWeapon(int weaponindex)
    {       
            weapons[currentWeapon].gameObject.SetActive(false);
            weapons[weaponindex].gameObject.SetActive(true);
            currentWeapon = weaponindex;        
    }
    public WeaponHandler GetCurrentWeapon()
    {
        return weapons[currentWeapon];
    }
    
    
}

