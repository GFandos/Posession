using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Switch : NetworkBehaviour
{

    [SerializeField] private bool playerInRange;
    private GameObject player;
    [SerializeField] private GameObject roundManager;
    public bool hasBehaviour;

    [ServerCallback]
    void Start()
    {
        playerInRange = false;
        roundManager = GameObject.FindWithTag("RoundManager");
    }

    [Server]
    public void PlayerInteracted()
    {

        if (playerInRange)
        {

            if(roundManager.GetComponent<RoundManager>().GetNumPlayers() >= 2)
            {
                roundManager.GetComponent<RoundManager>().StartRound();
            }
        }

    }

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }

    [ServerCallback]
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
