using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Goal : NetworkBehaviour
{

    public bool playerInRange;
    private GameObject player;
    [SerializeField] private GameObject roundManager;
    [SerializeField] private int playersInRange;

    [ServerCallback]
    void Start()
    {
        playerInRange = false;
        roundManager = GameObject.FindWithTag("RoundManager");
        playersInRange = 0;
    }

    [ServerCallback]
    void Update()
    {
        int numOfPlayers = roundManager.GetComponent<RoundManager>().GetNumPlayers();

        if((numOfPlayers > 0) && (numOfPlayers == playersInRange))
        {
            roundManager.GetComponent<RoundManager>().FinishRoundVictory();
        }
    }

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            playerInRange = true;
            player = other.gameObject;
            playersInRange++;
        }
    }

    [ServerCallback]
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playersInRange--;
        }
    }
}
