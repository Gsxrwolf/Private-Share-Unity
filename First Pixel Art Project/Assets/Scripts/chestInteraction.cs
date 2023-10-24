using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestInteraction : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("Open", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("Open", false);
    }
}
