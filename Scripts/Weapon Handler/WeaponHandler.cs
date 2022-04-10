using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AimType
{
    SelfAim,
    Aim,
    None
}
public enum FireType
{
    Single,
    Burst
}
public enum BulletType
{
    Bullet,
    Arrow,
    Spear,
    None
}
public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    
    [SerializeField]
    private ParticleSystem MuzzleFlash;
    [SerializeField]
    private AudioSource reloadSound, shootSound;
    public AimType aimType;
    public FireType fireType;
    public BulletType bulletType;
    public GameObject attackPoint;

    
    private void Start()
    {
      //  TurnOffAttackPoint();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.shootTrigger);
        
    }
    public void Aim(bool canAim) 
    {
        anim.SetBool(AnimationTags.aimParameter, canAim);
        
    }
    void TurnONMuzzle()
    {
        MuzzleFlash.Play();
    }
    void TurnOffMuzzle()
    {
        MuzzleFlash.Stop();
    }
    void PlayShootSound()
    {
        shootSound.Play();
    }
    void PlayReloadSound()
    {
        reloadSound.Play();
    }
    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {   
            attackPoint.SetActive(false);
        }        
    }
    
    
}

