using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private bool hasKey;
    public GameObject keyUI;

    public bool getHasKey()
    {
        return this.hasKey;
    }

    public void setHasKey(bool hasKey)
    {
        this.hasKey = hasKey;

        if (hasKey)
        {
            keyUI.SetActive(true);
        } else
        {
            keyUI.SetActive(false);
        }
    }

}
