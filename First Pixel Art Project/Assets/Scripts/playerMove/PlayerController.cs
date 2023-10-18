using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : PlayerManager
{

    private void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        gC = groundCheck.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float zRotation = transform.rotation.eulerAngles.z;
        animator.SetBool("walking", false);

        SprintCheck();
        InputCheck();
        AngelCheck(zRotation);
    }


    private void InputCheck()
    {
        if (Input.GetKey(KeyCode.A))
        {
            sR.flipX = true;
            animator.SetBool("walking", true);
            playerPos = transform.position;
            playerPos.x -= speed * Time.deltaTime;
            transform.position = playerPos;
        }
        if (Input.GetKey(KeyCode.D))
        {
            sR.flipX = false;
            animator.SetBool("walking", true);
            playerPos = transform.position;
            playerPos.x += speed * Time.deltaTime;
            transform.position = playerPos;
        }


        if (Input.GetKeyDown(KeyCode.W) && !jumping)
        {
            Jump();
            jumping = true;
            animator.SetBool("jumping", true);
            Vector2 force = Vector2.up;
            force.y += jumpForce * 500 * Time.deltaTime;
            rB.velocity = new Vector2(rB.velocity.x, 0);
            rB.AddForce(force);
        }
    }

    private void OnTriggerStay2D(Collider2D gC)
    {
        jumping = false;
    }

    private void OnTriggerExit2D(Collider2D gC)
    {
        animator.SetBool("jumping", false);
    }
}
