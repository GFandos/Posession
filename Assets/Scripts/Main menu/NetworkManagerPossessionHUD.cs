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
    public 

    void Awake()
    {
        manager = GetComponent<NetworkManagerPossession>();
    }

    public void OnHost()
    {
        manager.StartHost();
    }

    public void OnConnectAsClient()
    {
        manager.networkAddress = ipInput.text;
        manager.StartClient();
    }

}
