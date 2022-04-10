using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class UiManager : MonoBehaviour
{
    private static UiManager _instance = null;

    private UiManager()
    {

    }

    public static UiManager instance
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
                Debug.Log($"{nameof(UiManager)} has already a value, new value will be deleted");
                Destroy(value);
            }
        }

    }
    private void Awake()
    {
        _instance = this;
    }
    [Header("Connect", order = 0)]
    [SerializeField] private GameObject connectUi;
    [SerializeField] private InputField userNameField;
    
    
    public void connectClicked()
    {
        userNameField.interactable = false;
        connectUi.SetActive(false);
        NetworkManager.instance.Connect();
        
    }
    public void BackToMain()
    {
        userNameField.interactable = true;
        connectUi.SetActive(true);
    }
    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.reliable, (ushort)ClientToServerId.name);
        message.AddString(userNameField.text);
        NetworkManager.instance.client.Send(message);
    }
}
