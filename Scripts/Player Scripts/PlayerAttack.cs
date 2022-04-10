using RiptideNetworking;
using UnityEngine;
using System.Collections;
public class PlayerAttack : MonoBehaviour
{   

    public WeaponManager weaponManager;
    private Animator fpCamAnim;
    //private bool isZoomed;
    private Camera mainCam;
    private GameObject crosshair;
    private bool isAming = false;
    public float fireRate = 30f;
    public float nextTimeToFire;
    public float damage = 20f;
    [SerializeField]
    private GameObject arrowPrefab, spearPrefab, hitMarker, bulletHole;
    [SerializeField]
    private Transform arrowSpawnPoint;
    

    private void Awake()
    {
        
        weaponManager = GetComponent<WeaponManager>();
        fpCamAnim = transform.Find("ObjectRecoil").transform.Find(Tags.lookRoot).transform.Find(Tags.zoomCamera).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.crossHair);
        mainCam = Camera.main;  
    }
    void Start()
    {
       
    }

    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }
    private void FixedUpdate()
    {
        
    }
    void WeaponShoot()
    {
        if(weaponManager.GetCurrentWeapon().fireType == FireType.Burst)
        {
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 3f / fireRate;               
                weaponManager.GetCurrentWeapon().ShootAnimation();
                SendShootAnimation();
                GunRecoil.instance.Recoil();
                GunFire();
                
            }            
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                if (weaponManager.GetCurrentWeapon().tag == "Axe")
                {
                    weaponManager.GetCurrentWeapon().ShootAnimation();
                    SendShootAnimation();
                    
                }
                else if (weaponManager.GetCurrentWeapon().bulletType == BulletType.Bullet)
                {
                    weaponManager.GetCurrentWeapon().ShootAnimation();
                    SendShootAnimation();                    
                    GunRecoil.instance.Recoil();
                    GunFire();                                        
                }
                else
                {
                    if (isAming)
                    {
                        weaponManager.GetCurrentWeapon().ShootAnimation();
                        SendShootAnimation();
                        
                        if (weaponManager.GetCurrentWeapon().bulletType == BulletType.Arrow)
                        {
                            ThrowArrowSpear(true);
                        }
                        if (weaponManager.GetCurrentWeapon().bulletType == BulletType.Spear)
                        {
                            ThrowArrowSpear(false);
                        }
                    }
                }
            }
        }
        
    }//WeaoponShoot
    void ZoomInAndOut()
    {
        if (weaponManager.GetCurrentWeapon().aimType == AimType.Aim)
        {
            if(Input.GetMouseButtonDown(1) && weaponManager.GetCurrentWeapon().fireType == FireType.Single)
            {
                fpCamAnim.Play("ZoomSniper");
            }
            if (Input.GetMouseButtonDown(1) && weaponManager.GetCurrentWeapon().fireType == FireType.Burst)
            {
                fpCamAnim.Play(AnimationTags.zoomInAnim);
                crosshair.SetActive(false);
                //StartCoroutine(OnScoped());
            }
            if (Input.GetMouseButtonUp(1))
            {
                fpCamAnim.Play(AnimationTags.zoomOutAnim);
                crosshair.SetActive(true);
               // OnUnscoped();
            }
        }
        if(weaponManager.GetCurrentWeapon().aimType == AimType.SelfAim)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.GetCurrentWeapon().Aim(true);
                isAming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.GetCurrentWeapon().Aim(false);
                isAming = false;
            }
        }

    } //zoomInAndOut
    void GunFire()
    {        
        RaycastHit hitInfo;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hitInfo))
        {
            if (hitInfo.transform.CompareTag("Body") || hitInfo.transform.CompareTag("Head"))
            {
                Debug.Log($"we hit {hitInfo.transform.GetComponentInParent<Transform>().name}");
                ushort playerID = hitInfo.transform.GetComponentInParent<PlayerNetwork>().id;                
                float damage = this.damage;
                SendHitInfo(playerID, damage);
                hitMarker.transform.gameObject.SetActive(true);
                StartCoroutine(WaitForSeconds(0.1f));
            }
            else if (hitInfo.transform.CompareTag("Environment"))
            {
                GameObject obj = Instantiate(bulletHole, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                obj.transform.position += obj.transform.forward / 10000;
                StartCoroutine(DestroyAfter(obj));
            }
        }
    }
    void ThrowArrowSpear(bool throwArrow)
    {
        if (throwArrow) 
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.position = arrowSpawnPoint.position;
            arrow.GetComponent<ArrowAndSpear>().Lunch(mainCam);
        }
        else
        {
            GameObject spear = Instantiate(spearPrefab);
            spear.transform.position = arrowSpawnPoint.position;
            spear.GetComponent<ArrowAndSpear>().Lunch(mainCam);

        }
    }
    private void SendShootAnimation()
    {
        Message message = Message.Create(MessageSendMode.reliable, ClientToServerId.animations);        
        NetworkManager.instance.client.Send(message);
        
    }
    private IEnumerator WaitForSeconds(float seconds) 
    {
        yield return new WaitForSeconds(seconds);
        hitMarker.transform.gameObject.SetActive(false);
    }
    private IEnumerator DestroyAfter(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }
    private void SendHitInfo(ushort playerId, float damage)
    {

        Message message = Message.Create(MessageSendMode.reliable, ClientToServerId.hitRegister);
        message.AddUShort(playerId);
        message.AddFloat(damage);
        NetworkManager.instance.client.Send(message);
        
    }
} //class


