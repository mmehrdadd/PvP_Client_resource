using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
   // private Transform transform;
     public Vector3 currentRotation;
     public Vector3 targetRotation;
     public GameObject player;
    
    private float recoilx = -12f,
                  recoily = 2f,
                  recoilz = 2f;

    private float returnSpeed = 1.5f,
                  snappiness = 6f;

    public static GunRecoil instance { get; private set; }
    

    private void Awake()
    {
        instance = this;
        //transform = GetComponent<Transform>();
    }


    void Update()
    {
        
        targetRotation = Vector3.Slerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
        
    }

    public void Recoil()
    {       
        if(player.GetComponent<WeaponManager>().GetCurrentWeapon().fireType == FireType.Burst)
        targetRotation += new Vector3(recoilx/6, Random.Range(-recoily/1.5f, recoily/1.5f), Random.Range(-recoilz, recoilz));
        else
        {
            targetRotation += new Vector3(recoilx, Random.Range(-recoily, recoily), Random.Range(-recoilz, recoilz));
        }
       
    }

      
    

    

}
