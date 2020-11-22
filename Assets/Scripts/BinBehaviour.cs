using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinBehaviour : MonoBehaviour
{

    private Animator animator;
    public AudioSource audioSource;
    public bool canPlay;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open()
    {

        if (!animator.GetBool("opened"))
        {
            animator.SetBool("opened", true);
        }

        //audioSource.Play();

    }

}
