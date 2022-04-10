using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
    private EnemySpawner() { }

    private static EnemySpawner _instance;

    public static EnemySpawner instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new EnemySpawner();
                _instance.GetComponent<EnemySpawner>();                
            }
            return _instance;
        }        
    }
    private void Awake()
    {
        _instance = this;
    }
}
