using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    public AudioSource audioSource;
    public bool canPlay;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    public void Open()
    {

        if (!animator.GetBool("opened"))
        {
            animator.SetBool("opened", true);
            collider.enabled = false;
        }

        //audioSource.Play();

    }
}
