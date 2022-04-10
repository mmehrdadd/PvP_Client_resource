using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using System;


public enum ServerToClient : ushort
{
    playerSpawned = 1,
    playerMovement,
    selectedWeapon,
    playerSpeedCrouch,
    animation,
    hitRegister,
    playerRespawn,
}
public enum ClientToServerId : ushort
{
    name = 1,
    input,
    playerRotation,
    selectedWeapon,
    animations,
    hitRegister,
}
public sealed class NetworkManager : MonoBehaviour
{
    private static NetworkManager _instance = null;

    private NetworkManager()
    {

    }

    public static NetworkManager instance
    {

        get => _instance;
        set
        {
            if (_instance == null)
            {
                _instance = value;
            }
            else if(_instance != value)
            {
                Debug.Log($"{nameof(NetworkManager)} has already a value, new value will be deleted");
                Destroy(value);
            }
        }
        
    }
    private void Awake()
    {
        _instance = this;
    }
    public Client client { get; private set; }

    [SerializeField] private string ip;
    [SerializeField] private ushort port;

    
    private void Start()
    {
        
        Application.targetFrameRate = 60;
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        client = new Client();
        client.Connected += DidConnect;
        client.ConnectionFailed += FailedToConnect;
        client.Disconnected += DidDisconnect;
        client.ClientDisconnected += PlayerLeft;    
       
    }
    private void FixedUpdate()
    {
        client.Tick();
    }
    private void OnApplicationQuit()
    {
        client.Disconnect();
    }
    public void Connect()
    {
        client.Connect($"{ip}:{port}");
    }
    private void DidConnect(object sender, EventArgs e)
    {
        UiManager.instance.SendName();
    }
    private void FailedToConnect(object sender, EventArgs e)
    {
        UiManager.instance.BackToMain();
    }
    private void DidDisconnect(object sender, EventArgs e)
    {
        UiManager.instance.BackToMain();
    }
    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        Destroy(PlayerNetwork.players[e.Id].gameObject);
    }

}
