using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Mirror;
using System.ComponentModel;

public class Player : NetworkBehaviour
{
    [SerializeField] public GameObject[] keyContainers;
    [SerializeField] private bool playerInRangeOfInteraction;
    [SerializeField] public GameObject keyUI;

    private GameObject canvas;
    private GameObject gameObjectInRange;

    void Start()
    {
        canvas = this.GetComponentInChildren<Canvas>(true).gameObject;

        if (isLocalPlayer)
        {
            canvas.SetActive(true);
            CmdSetKeyToContainers();

            playerInRangeOfInteraction = false;
        } else
        {
            canvas.SetActive(false);
        }

    }

    [ClientCallback]
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("interact") && isLocalPlayer)
        {
            if(isServer)
                PlayerInteraction();
            else if (isClient)
                CmdInteract();
        }
    }

    [ClientCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Container") || other.CompareTag("Gate"))
        {
            playerInRangeOfInteraction = true;
            gameObjectInRange = other.gameObject;
        }
    }

    [ClientCallback]
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Container") || other.CompareTag("Gate"))
        {
            playerInRangeOfInteraction = false;
            gameObjectInRange = null;
        }
    }

    [Command]
    private void CmdSetKeyToContainers()
    {
        System.Random rnd = new System.Random();
        int containersLength = this.keyContainers.Length;
        int randNum = rnd.Next(0, containersLength);

        for (int i = 0; i < containersLength; i++)
        {

            if (i == randNum)
            {
                keyContainers[i].GetComponent<ItemInventory>().SetKey(true);
            }
            else
            {
                keyContainers[i].GetComponent<ItemInventory>().SetKey(false);
            }

        }

    }
    
    [Command]
    void CmdInteract()
    {
        PlayerInteraction();
    }

    [Server]
    void PlayerInteraction()
    {
        if (playerInRangeOfInteraction && gameObjectInRange != null)
        {
        
            if (gameObjectInRange.CompareTag("Container"))
                gameObjectInRange.GetComponent<Container>().PlayerInteracted();
            else if (gameObjectInRange.CompareTag("Gate"))
                gameObjectInRange.GetComponent<Gate>().PlayerInteracted();

        }
    }

    [ClientRpc]
    public void RpcSetKeyUI(bool keyGiven)
    {
        if (isLocalPlayer)
        {
            keyUI.SetActive(keyGiven);
        }
    }

}
