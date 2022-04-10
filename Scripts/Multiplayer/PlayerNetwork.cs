using RiptideNetworking;
using UnityEngine;
using System.Collections.Generic;
public class PlayerNetwork : MonoBehaviour
{
    public static Dictionary<ushort, PlayerNetwork> players = new Dictionary<ushort, PlayerNetwork>();

    public ushort id { get; private set; }
    public bool isLocal { get; private set; }
    private string userName;
    [SerializeField] private Transform camTransform;
    [SerializeField] private Transform lookRoot;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private HealthScript healthScript;
    [SerializeField] private WeaponManagerEnemy weaponManagerEnemy;
    [SerializeField] private PlayerAttackEnemy playerAttackEnemy;

    private void OnDestroy()
    {
        players.Remove(id);
    }
    
    private void Move(Vector3 newPosition, Vector3 forward, Vector3 newHeightPosition, float height)
    {
        transform.position = newPosition;
        lookRoot.localPosition = newHeightPosition;
        characterController.height = height;

        if (!isLocal)
        {
            camTransform.forward = forward;
        }       
    }

    public static void Spawn(ushort id, string username, Vector3 position)
    {
        PlayerNetwork player = new PlayerNetwork();
        if (id == NetworkManager.instance.client.Id)
        {
            player = Instantiate(GameLogic.instance.localPlayerPrefab, position, Quaternion.identity).GetComponent<PlayerNetwork>();
            player.isLocal = true;
        }
        else
        {
            player = Instantiate(GameLogic.instance.PlayerPrefab, position, Quaternion.identity).GetComponent<PlayerNetwork>();
        }
        player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
        player.id = id;
        player.userName = username;
        players.Add(id, player);
    }

    [MessageHandler((ushort)ServerToClient.playerSpawned)]
    private static void SpawnPlayer(Message message)
    {
        Spawn(message.GetUShort(), message.GetString(), message.GetVector3());
    }  

    [MessageHandler((ushort)ServerToClient.playerMovement)]
    private static void PlayerMovement(Message message)
    {
        if(players.TryGetValue(message.GetUShort(), out PlayerNetwork player))
        {           
            player.Move(message.GetVector3(), message.GetVector3(), message.GetVector3(), message.GetFloat());           
        }
    }

   [MessageHandler((ushort)ServerToClient.selectedWeapon)]
   private static void SelectGun(Message message)
    {
        if (players.TryGetValue(message.GetUShort(), out PlayerNetwork player))
        {
            if (!player.isLocal)
                player.weaponManagerEnemy.TurnOnSelectedWeapon(message.GetInt());
        }
    }

    [MessageHandler((ushort)ServerToClient.animation)]
    private static void Animation(Message message)
    {
        if (players.TryGetValue(message.GetUShort(), out PlayerNetwork player))
        {            
            if(!player.isLocal)
                player.playerAttackEnemy.PlayShootAnimation();            
        }
    }
    [MessageHandler((ushort)ServerToClient.hitRegister)]
    private static void DealDamage(Message message)
    {
        if(players.TryGetValue(message.GetUShort(), out PlayerNetwork player))
        {
            player.healthScript.ApplyDamage(message.GetFloat());
        }
    }
    [MessageHandler((ushort)ServerToClient.playerRespawn)]
    private static void PlayerRespawn(Message message)
    {
        if(players.TryGetValue(message.GetUShort(), out PlayerNetwork player))
        {
            player.healthScript.Death();
            player.transform.position = new Vector3(0.289326906f, 5.079f, -1.49843836f);
        }
    }
}

    