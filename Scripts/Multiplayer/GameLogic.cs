using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private static GameLogic _instance = null;

    private GameLogic()
    {

    }

    public static GameLogic instance
    {

        get => _instance;
        set
        {
            if (_instance == null)
            {
                _instance = value;
            }
            else if (_instance != value)
            {
                Debug.Log($"{nameof(GameLogic)} has already a value, new value will be deleted");
                Destroy(value);
            }
        }

    }
    private void Awake()
    {
        _instance = this;
    }

    public GameObject localPlayerPrefab
    {
        get
        {
            return _localPlayerPrefab;
        }
    }
    public GameObject PlayerPrefab
    {
        get
        {
            return _PlayerPrefab;
        }
    }
    [Header("prefabs")]
    [SerializeField] private GameObject _localPlayerPrefab;
    [SerializeField] private GameObject _PlayerPrefab;
}
