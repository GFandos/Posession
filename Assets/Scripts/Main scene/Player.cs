using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField] private GameObject[] initialAreaKeyContainers;
    [SerializeField] private GameObject[] firstAreaKeyContainers;
    [SerializeField] private GameObject[] SecondAreaKeyContainers;
    [SerializeField] private bool playerInRangeOfInteraction;
    [SerializeField] public GameObject keyUI;
    [SerializeField] public TMP_Text timerText;
    [SerializeField] private GameObject roundManager;

    private GameObject canvas;
    private GameObject gameObjectInRange;

    [ClientCallback]
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>(true).gameObject;

        initialAreaKeyContainers = GameObject.FindGameObjectsWithTag("InitialContainer");
        firstAreaKeyContainers = GameObject.FindGameObjectsWithTag("FirstAreaContainer");
        SecondAreaKeyContainers = GameObject.FindGameObjectsWithTag("SecondAreaContainer");
        roundManager = GameObject.FindWithTag("RoundManager");

        if (isLocalPlayer)
        {
            CmdSetKeyToContainers();
            SetGateKeysNeeded();
            SetPlayerNum();

            playerInRangeOfInteraction = false;
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }

    }

    [Command]
    void SetGateKeysNeeded()
    {
        GameObject[] gates = GameObject.FindGameObjectsWithTag("Gate");
        foreach (GameObject gate in gates)
        {
            gate.GetComponent<Gate>().IncreaseKeysNeeded();
        }
    }

    [Command]
    void SetPlayerNum()
    {
        roundManager.GetComponent<RoundManager>().IncreasePlayerNum();
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

        if(isLocalPlayer)
        {
            if(isServer)
                UpdateTimer();

            if (Input.GetButtonDown("interact"))
            {
                if (isServer)
                {
                    PlayerInteraction();
                }
                else if (isClient)
                {
                    CmdInteract();
                }

            }

        }

    }

    [Server]
    void UpdateTimer()
    {
        float currentTime = roundManager.GetComponent<RoundManager>().GetCurrentTime();
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        RpcUpdateTimer(minutes, seconds);
    }

    [ClientCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InitialContainer") || other.CompareTag("FirstAreaContainer") || other.CompareTag("SecondAreaContainer") || other.CompareTag("Gate") || other.CompareTag("Switch"))
        {
            playerInRangeOfInteraction = true;
            gameObjectInRange = other.gameObject;
        }
    }

    [ClientCallback]
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InitialContainer") || other.CompareTag("FirstAreaContainer") || other.CompareTag("SecondAreaContainer") || other.CompareTag("Gate") || other.CompareTag("Switch"))
        {
            playerInRangeOfInteraction = false;
            gameObjectInRange = null;
        }
    }

    [Command]
    private void CmdSetKeyToContainers()
    {
        System.Random rnd = new System.Random();
        int playerNetworkId = GetComponent<NetworkIdentity>().GetInstanceID();
        int initialRandNum = rnd.Next(0, initialAreaKeyContainers.Length);
        int FirstRandNum = rnd.Next(0, firstAreaKeyContainers.Length);
        int SecondRandNum = rnd.Next(0, SecondAreaKeyContainers.Length);

        Debug.Log("Initial key set to: " + initialAreaKeyContainers[initialRandNum]);
        initialAreaKeyContainers[initialRandNum].GetComponent<ItemInventory>().SetKey(true);
        initialAreaKeyContainers[initialRandNum].GetComponent<ItemInventory>().SetPlayerId(playerNetworkId);

        Debug.Log("First key set to: " + firstAreaKeyContainers[FirstRandNum]);
        firstAreaKeyContainers[FirstRandNum].GetComponent<ItemInventory>().SetKey(true);
        firstAreaKeyContainers[FirstRandNum].GetComponent<ItemInventory>().SetPlayerId(playerNetworkId);

        /*SecondAreaKeyContainers[SecondRandNum].GetComponent<ItemInventory>().SetKey(true);
        SecondAreaKeyContainers[SecondRandNum].GetComponent<ItemInventory>().SetPlayerId(playerNetworkId);*/
    }

    [Command]
    void CmdInteract()
    {
        PlayerInteraction();
    }

    [Server]
    void PlayerInteraction()
    {
        Debug.Log("Player interacted");
        if (playerInRangeOfInteraction && gameObjectInRange != null)
        {
            Debug.Log("Is switch? " + gameObjectInRange.CompareTag("Switch"));
            int playerNetworkId = GetComponent<NetworkIdentity>().GetInstanceID();

            if (gameObjectInRange.CompareTag("InitialContainer") || gameObjectInRange.CompareTag("FirstAreaContainer") || gameObjectInRange.CompareTag("SecondAreaContainer"))
                gameObjectInRange.GetComponent<Container>().PlayerInteracted(playerNetworkId);
            else if (gameObjectInRange.CompareTag("Gate"))
                gameObjectInRange.GetComponent<Gate>().PlayerInteracted();
            else if (gameObjectInRange.CompareTag("Switch"))
                gameObjectInRange.GetComponent<Switch>().PlayerInteracted();

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

    [ClientRpc]
    public void RpcUpdateTimer(float minutes, float seconds)
    {
            timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

}