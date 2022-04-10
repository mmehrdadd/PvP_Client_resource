using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float attackRadius = 1f;
    public float damage = 25f;
    public LayerMask layerMask;
    void FixedUpdate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, layerMask);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<HealthScript>().ApplyDamage(damage);
            Debug.Log("hit");
        }
    }


}
