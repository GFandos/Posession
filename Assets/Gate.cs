using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
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
            if (player.GetComponent<PlayerInventory>().getHasKey())
            {
                player.GetComponent<PlayerInventory>().setHasKey(false);
                this.GetComponent<GateBehaviour>().Open();
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
