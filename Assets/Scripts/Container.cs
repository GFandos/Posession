using UnityEngine;
using Mirror;

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
    public void PlayerInteracted()
    {

        if(playerInRange)
        {
            if (hasBehaviour)
                this.GetComponent<ContainerBehaviour>().DoBehaviour();

            if (this.GetComponent<ItemInventory>().GetKey())
            {
                player.GetComponent<PlayerInventory>().SetHasKey(true);
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
