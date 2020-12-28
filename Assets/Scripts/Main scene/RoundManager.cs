using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoundManager : NetworkBehaviour
{

    [SerializeField] private int numPlayers = 0;
    [SerializeField] private bool roundStarted = false;
    [SerializeField] private bool gameFailed = false;
    [SerializeField] private bool victory = false;
    [SerializeField] private float currentTime = 60f;
    public float startingTime = 60f;

    [ServerCallback]
    void Start()
    {
        currentTime = startingTime;
    }

    [ServerCallback]
    void Update()
    {
        if(!gameFailed && roundStarted)
            currentTime -= 1 * Time.deltaTime;

        if(currentTime <= 0)
            gameFailed = true;

        if (gameFailed || victory)
        {
            //RpcChangeScene();
            ChangeScene();
        }

    }

    [ClientRpc]
    private void RpcChangeScene()
    {
        GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManagerPossessionHUD>().ChangeScene();
    }

    [Server]
    public float GetCurrentTime()
    {
        return currentTime;
    }

    [Server]
    public void IncreasePlayerNum()
    {
        numPlayers++;
    }

    [Server]
    public void StartRound()
    {
        roundStarted = true;
    }

    [Server]
    public int GetNumPlayers()
    {
        return numPlayers;
    }

    [Server]
    public bool GetStartRound()
    {
        return roundStarted;
    }

    [Server]
    public void FinishRoundVictory()
    {
        victory = true;
    }

    private void ChangeScene()
    {
        GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManagerPossessionHUD>().ChangeScene();
    }
}
