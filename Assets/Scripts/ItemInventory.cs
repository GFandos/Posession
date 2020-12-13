using UnityEngine;
using Mirror;

public class ItemInventory : NetworkBehaviour
{
    [SerializeField] private bool key;

    [Server]
    public bool GetKey()
    {
        return key;
    }

    [Server]
    public void SetKey(bool hasKey)
    {
        key = hasKey;
    }

}
