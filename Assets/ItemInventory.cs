using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private bool key;

    public bool getKey()
    {
        return this.key;
    }

    public void setKey(bool hasKey)
    {
        this.key = hasKey;
    }

}
