using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerPossessionHUD : MonoBehaviour
{

    NetworkManagerPossession manager;
    public TMP_InputField ipInput;

    void Awake()
    {
        manager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManagerPossession>();
    }

    public void OnHost()
    {
        Debug.Log("OnHost pressed");
        manager.StartHost();
    }

    public void OnConnectAsClient()
    {
        manager.networkAddress = ipInput.text;
        manager.StartClient();
    }

    [Server]
    public void ChangeScene()
    {
        if (NetworkServer.active && NetworkClient.active)
        {
            manager.StopHost();
        }
    }

}
