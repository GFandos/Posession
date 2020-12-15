using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Gate : NetworkBehaviour
{

    public bool playerInRange;
    private GameObject player;
    [SerializeField] private int keysNeeded;

    [ServerCallback]
    void Start()
    {
        playerInRange = false;
        keysNeeded = 0;
    }

    /*[ServerCallback]
    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange && player.GetComponent<PlayerInventory>().GetHasKey())
        {
            GetComponent<GateBehaviour>().Open();
            player.GetComponent<PlayerInventory>().SetHasKey(false);
        }
    }*/

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

    [Server]
    public void PlayerInteracted()
    {

        if (playerInRange && player.GetComponent<PlayerInventory>().GetHasKey())
        {

            keysNeeded--;

            if(keysNeeded == 0)
            {
                GetComponent<GateBehaviour>().Open();
            }

            player.GetComponent<PlayerInventory>().SetHasKey(false);

        }

    }

    [Server]
    public int GetKeysNeeded()
    {
        return keysNeeded;
    }

    [Server]
    public void IncreaseKeysNeeded()
    {
        keysNeeded++;
    }
}
