using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{

    public bool isPlayer,
                isCannibal,
                isBoar;
    public float health;
    private EnemyAnimationScript enemyAnim;
    [SerializeField] private ParticleSystem playerdeath;
    private NavMeshAgent navAgent;
    private EnemyScript enemy;
    private bool isDead;
    private EnemyState enemyState;
    private EnemyAudio enemyAudio;
    private PlayerUI playerStats;
    void Awake()
    {
               
        if(isBoar || isCannibal)
        {
            enemyAudio = GetComponentInChildren<EnemyAudio>();
            enemy = GetComponent<EnemyScript>();
            enemyAnim = GetComponent<EnemyAnimationScript>();
            navAgent = GetComponent<NavMeshAgent>();
        }
        if (isPlayer)
        {
            playerStats = GetComponent<PlayerUI>();
        }
    }
    public void ApplyDamage(float newHealth)
    {
        if (isDead)
        {
            return;
        }
        health = newHealth ;
        if (isPlayer)
        {
            playerStats.UpdateHealthBar(health);
        }
        if(isBoar || isCannibal)
        {
            if(enemyState == EnemyState.Idle)
            {
                enemy.chaseDistance = 50f;
            }
        }
        if (health <= 0)
        {
            Death();
        }
        
    }
   
   public void Death()
    {
        isDead = true;
        if (isCannibal)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(DieSound());
            enemyAnim.Dead();
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy.enabled = false;
        }
        if (isBoar)
        {
            StartCoroutine(DieSound());
            enemyAnim.Dead();
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy.enabled = false;
            
        }
        if (isPlayer)
        {
            playerdeath.Play();            
        }
        #region notnecessary
        //if (isPlayer)
        //{

        //    //GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.enemyTag);
        //    //foreach(GameObject enemy in enemies)
        //    //{
        //    //    enemy.GetComponent<EnemyScript>().enabled = false;
        //    //}
        //    //GetComponent<PlayerMovement>().enabled = false;
        //    //GetComponent<PlayerAttack>().enabled = false;
        //    //GetComponent<WeaponManager>().GetCurrentWeapon().gameObject.SetActive(false);
        //}
        //if(tag == Tags.playerTag)
        //{
        //    //Invoke("RestartGame", 3f);
        //}
        //else
        //{
        //    //Invoke("DestroyGameObject", 3f);
        //}
        #endregion
    }
    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    private void DestroyGameObject()
    {
        gameObject.SetActive(false);
    }
    
    IEnumerator DieSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.DieSound();
    }
}
