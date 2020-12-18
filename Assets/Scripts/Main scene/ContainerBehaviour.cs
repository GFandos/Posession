using Mirror;
using UnityEngine;

public class ContainerBehaviour : NetworkBehaviour
{

    private Animator animator;
    public AudioSource audioSource;
    public bool canPlay;

    [ServerCallback]
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    [Server]
    public void DoBehaviour()
    {
        if (!animator.GetBool("opened"))
        {
            animator.SetBool("opened", true);
        }
    }

}
