using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVBehaviour : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnOff()
    {

        if(animator.GetBool("on"))
        {
            animator.SetBool("on", false);
        } else
        {
            animator.SetBool("on", true);
        }
    }

}
