using UnityEngine;
using RiptideNetworking;
public class PlayerAttackEnemy : MonoBehaviour
{
    [SerializeField] private WeaponManagerEnemy weaponManager;

    //void WeaponShoot()
    //{
    //    if(weaponManager.GetCurrentWeapon().fireType == FireType.Burst)
    //    {
    //        if(Input.GetMouseButton(0) && Time.time > nextTimeToFire)
    //        {
    //            nextTimeToFire = Time.time + 1f / fireRate;               
    //            weaponManager.GetCurrentWeapon().ShootAnimation();
    //            GunRecoil.instance.Recoil();
    //            GunFire();               
    //        }            
    //    }
    //    else
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {

    //            if (weaponManager.GetCurrentWeapon().tag == "Axe")
    //            {
    //                weaponManager.GetCurrentWeapon().ShootAnimation();

    //            }
    //            else if (weaponManager.GetCurrentWeapon().bulletType == BulletType.Bullet)
    //            {
    //                weaponManager.GetCurrentWeapon().ShootAnimation();
    //                GunRecoil.instance.Recoil();
    //                GunFire();


    //            }
    //            else
    //            {
    //                if (isAming)
    //                {
    //                    weaponManager.GetCurrentWeapon().ShootAnimation();
    //                    if(weaponManager.GetCurrentWeapon().bulletType == BulletType.Arrow)
    //                    {
    //                        ThrowArrowSpear(true);
    //                    }
    //                    if (weaponManager.GetCurrentWeapon().bulletType == BulletType.Spear)
    //                    {
    //                        ThrowArrowSpear(false);
    //                    }
    //                }
    //            }
    //        }
    //    }

    //}//WeaoponShoot
    //void ZoomInAndOut()
    //{
    //    if (weaponManager.GetCurrentWeapon().aimType == AimType.Aim)
    //    {
    //        if(Input.GetMouseButtonDown(1) && weaponManager.GetCurrentWeapon().fireType == FireType.Single)
    //        {
    //            fpCamAnim.Play(AnimationTags.zoomInAnimSniper);
    //        }
    //        else if (Input.GetMouseButtonDown(1) && weaponManager.GetCurrentWeapon().fireType == FireType.Burst)
    //        {
    //            fpCamAnim.Play(AnimationTags.zoomInAnim);
    //            crosshair.SetActive(false);
    //            //StartCoroutine(OnScoped());
    //        }
    //        if (Input.GetMouseButtonUp(1))
    //        {
    //            fpCamAnim.Play(AnimationTags.zoomOutAnim);
    //            crosshair.SetActive(true);
    //           // OnUnscoped();
    //        }
    //    }
    //    if(weaponManager.GetCurrentWeapon().aimType == AimType.SelfAim)
    //    {
    //        if (Input.GetMouseButtonDown(1))
    //        {
    //            weaponManager.GetCurrentWeapon().Aim(true);
    //            isAming = true;
    //        }
    //        if (Input.GetMouseButtonUp(1))
    //        {
    //            weaponManager.GetCurrentWeapon().Aim(false);
    //            isAming = false;
    //        }
    //    }

    //} //zoomInAndOut
    //void GunFire()
    //{

    //    RaycastHit hit;
    //    if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
    //    {
    //        Debug.Log($"we hit { hit.transform.name }");    
    //        hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
    //    }
    //}
    //void ThrowArrowSpear(bool throwArrow)
    //{
    //    if (throwArrow) 
    //    {
    //        GameObject arrow = Instantiate(arrowPrefab);
    //        arrow.transform.position = arrowSpawnPoint.position;
    //        arrow.GetComponent<ArrowAndSpear>().Lunch(mainCam);
    //    }
    //    else
    //    {
    //        GameObject spear = Instantiate(spearPrefab);
    //        spear.transform.position = arrowSpawnPoint.position;
    //        spear.GetComponent<ArrowAndSpear>().Lunch(mainCam);

    //    }
    //}

    public void PlayShootAnimation()
    {
        weaponManager.GetCurrentWeapon().ShootAnimation();

    }
  
    
}
