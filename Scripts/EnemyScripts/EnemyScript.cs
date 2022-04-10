using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyState{
    Idle,
    Chase,
    Attack
}
public class EnemyScript : MonoBehaviour
{
    private EnemyState enemyState;
    private EnemyAnimationScript enemyAnim;
    private NavMeshAgent navAgent;
    private Transform target;
    public float walkSpeed = 0.5f,
                 runSpeed = 4f,
                 chaseDistance = 7f,
                 attackDistance = 1.8f,
                 chaseAfterAttackDistance = 2f,
                 patrolRadiusMin = 20f,
                 patrolRadiusMax = 60f,
                 patrolForThisTime = 15f,
                 waitForAttack = 2f;

    private float currentChaseDistance,
                  patrolTimer,
                  attackTimer;
    public GameObject attackPoint;
    private EnemyAudio enemyAudio;
    private void Awake()
    {
        enemyAudio = GetComponentInChildren<EnemyAudio>();
        enemyAnim = GetComponent<EnemyAnimationScript>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.playerTag).transform;
    }
    void Start()
    {
        enemyState = EnemyState.Idle;
        patrolTimer = patrolForThisTime;
        currentChaseDistance = chaseDistance;   
    }

    
    void Update()
    {
        if(enemyState == EnemyState.Idle)
        {
            Patrol();
        }
        if (enemyState == EnemyState.Chase)
        {
            Chase();
        }
        if (enemyState == EnemyState.Attack)
        {
            Attack();
        }
    }
    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        patrolTimer += Time.deltaTime;
        if(patrolTimer > patrolForThisTime)
        {
            SetRandomDestination();

            patrolTimer = 0;
        }
        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Walk(true);
        }
        else
        {
            enemyAnim.Walk(false);
        }
        if(Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyState = EnemyState.Chase;
            enemyAudio.ScreamSound();

        }
    }

    public void SetRandomDestination()
    {
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randDir = Random.insideUnitSphere * randRadius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);
        navAgent.SetDestination(navHit.position);
            
    }

    private void Chase()    
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        enemyAnim.Run(true);
        enemyAnim.Walk(false);
        navAgent.SetDestination(target.position);
        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            enemyState = EnemyState.Attack;
            enemyAnim.Run(false);
            enemyAnim.Walk(false);
        }
    }
    private void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        
        attackTimer += Time.deltaTime;
        if(attackTimer >= waitForAttack)
        {
            enemyAudio.EnemyAttackSounds();
            enemyAnim.Attack();
            attackTimer = 0;
        }
        if (Vector3.Distance(transform.position, target.position) > attackDistance + chaseAfterAttackDistance)
        {
            enemyState = EnemyState.Chase;
        }
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
