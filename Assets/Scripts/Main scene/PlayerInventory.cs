using Mirror;
using UnityEngine;

public class PlayerInventory : NetworkBehaviour
{

    [SerializeField] private bool hasKey;

    [Server]
    public bool GetHasKey()
    {
        return this.hasKey;
    }

    [Server]
    public void SetHasKey(bool keyGiven)
    {
        hasKey = keyGiven;
        GetComponent<Player>().RpcSetKeyUI(hasKey);
    }
}
