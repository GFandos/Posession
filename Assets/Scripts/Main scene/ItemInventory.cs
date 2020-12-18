using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class ItemInventory : NetworkBehaviour
{
    [SerializeField] private bool key;
    [SerializeField] private List<int> playerIds;

    [ServerCallback]
    void Start()
    {
        playerIds = new List<int>();
        key = false;
    }

    [Server]
    public bool GetKey()
    {
        return key;
    }

    [Server]
    public List<int> GetPlayerIds()
    {
        return playerIds;
    }
     
    [Server]
    public void SetKey(bool hasKey)
    {
        key = hasKey;
    }

    [Server]
    public void SetPlayerId(int id)
    {
        playerIds.Add(id);
    }

    public int RemovePlayerId(int id)
    {
        playerIds.Remove(id);
        return playerIds.Count;
    }

}
