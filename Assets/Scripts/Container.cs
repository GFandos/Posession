using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class Container : NetworkBehaviour
{
    [SerializeField] private bool playerInRange;
    private GameObject player;
    public bool hasBehaviour; 

    [ServerCallback]
    void Start()
    {
        playerInRange = false;
    }

    [Server]
    public void PlayerInteracted(int playerId)
    {

        if(playerInRange)
        {
            if (hasBehaviour)
                this.GetComponent<ContainerBehaviour>().DoBehaviour();

            List<int> itemPlayerIds = GetComponent<ItemInventory>().GetPlayerIds();
            bool itemHasKey = GetComponent<ItemInventory>().GetKey();

            if (itemHasKey && itemPlayerIds.Contains(playerId))
            {
                player.GetComponent<PlayerInventory>().SetHasKey(true);
                int idsLeft = GetComponent<ItemInventory>().RemovePlayerId(playerId);

                if(idsLeft == 0) 
                    GetComponent<ItemInventory>().SetKey(false);
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
