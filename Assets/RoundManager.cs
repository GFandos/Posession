using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoundManager : NetworkBehaviour
{

    [SerializeField] private int numPlayers = 0;
    [SerializeField] private bool roundStarted = false;
    [SerializeField] private bool gameFailed = false;
    private float currentTime = 0f;
    private float startingTime = 60f;

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
        {
            gameFailed = true;
            NetworkManager.singleton.StopHost();
        }
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
}
