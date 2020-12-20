using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerPossessionHUD : NetworkBehaviour
{

    //NetworkManagerPossession manager;
    public TMP_InputField ipInput;

    void Awake()
    {
        //manager = GetComponent<NetworkManagerPossession>();
    }

    public void OnHost()
    {
        Debug.Log("OnHost pressed");
        GetComponent<NetworkManagerPossession>().StartHost();
    }

    public void OnConnectAsClient()
    {
        GetComponent<NetworkManagerPossession>().networkAddress = ipInput.text;
        GetComponent<NetworkManagerPossession>().StartClient();
    }

    [Server]
    public void ChangeScene(string scene)
    {
        GetComponent<NetworkManagerPossession>().EndHosting();
    }

}
