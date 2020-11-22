using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bin : MonoBehaviour
{

    public bool playerInRange;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
        {
            this.GetComponent<BinBehaviour>().Open();
            if(this.GetComponent<ItemInventory>().getKey())
            {
                player.GetComponent<PlayerInventory>().setHasKey(true);
                this.GetComponent<ItemInventory>().setKey(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            this.player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
