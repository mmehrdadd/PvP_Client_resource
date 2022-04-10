using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAndSpear : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float speed = 30f;
    public float damage = 20f;
    public float deactive_Timer = 3f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Invoke("DeactivateGameObject", deactive_Timer);
    }

    
    void Update()
    {
        
    }
    private void DeactivateGameObject()
    {
        if(gameObject.activeInHierarchy)
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider target)
    {
        
    }
     public void Lunch(Camera mainCamera)
     {
        rigidbody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + rigidbody.velocity);
     }
}
