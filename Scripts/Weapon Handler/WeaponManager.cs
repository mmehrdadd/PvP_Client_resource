using RiptideNetworking;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    public  WeaponHandler[] weapons;
    public int currentWeapon;
    [SerializeField] private GameObject crossHair;
    void Start()
    {        
        currentWeapon = 0;
        weapons[currentWeapon].gameObject.SetActive(true);        
        crossHair.SetActive(false);
    }

        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            crossHair.SetActive(false);
            TurnOnSelectedWeapon(0);            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            crossHair.SetActive(true);
            TurnOnSelectedWeapon(1);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            crossHair.SetActive(true);
            TurnOnSelectedWeapon(2);            
        }
        
    }
    void TurnOnSelectedWeapon(int weaponindex)
    {
        if(currentWeapon != weaponindex)
        {
            weapons[currentWeapon].gameObject.SetActive(false);
            weapons[weaponindex].gameObject.SetActive(true);
            currentWeapon = weaponindex;
            SendSelectedWeapon();
        }        
    }
    public WeaponHandler GetCurrentWeapon()
    {
        return weapons[currentWeapon];
    }
    public void SendSelectedWeapon()
    {
        Message message = Message.Create(MessageSendMode.reliable, ClientToServerId.selectedWeapon);
        message.AddInt(currentWeapon);
        NetworkManager.instance.client.Send(message);
    }
}
