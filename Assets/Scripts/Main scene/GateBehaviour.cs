using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : NetworkBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] public BoxCollider2D boxCollider;

    public AudioSource audioSource;
    public bool canPlay;

    [Server]
    public void Open()
    {

        if (!animator.GetBool("opened"))
        {
            animator.SetBool("opened", true);
            RpcDisableCollider();
        }

    }

    [ClientRpc]
    private void RpcDisableCollider()
    {
        boxCollider.enabled = false;
    }
}
