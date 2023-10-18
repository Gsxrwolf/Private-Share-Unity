using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 playerPos;
    public Animator animator;
    public AudioSource jumpSound;
    public GameObject groundCheck;
    private Rigidbody2D rB;
    private SpriteRenderer sR;
    private BoxCollider2D gC;


    float speed = 1f;
    [SerializeField] float normalSpeed = 1f;
    [SerializeField] float sprintSpeed = 2f;
    [SerializeField] float jumpForce = 1.5f;
    [SerializeField] float maxRotation = 10f;
    [SerializeField] bool sprint = false;
    [SerializeField] bool jumping = false;
    [SerializeField] float jumpTime;


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
        animator.SetBool("jumping", false);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
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


        if (Input.GetKey(KeyCode.W) && !jumping)
        {
            jumping = true;
            animator.SetBool("jumping", true);
            Vector2 force = Vector2.up ;
            force.y += jumpForce * 500 * Time.deltaTime;
            rB.AddForce(force);
        }

        if (zRotation > maxRotation || zRotation < -maxRotation)
        {
            rB.angularVelocity = 0f;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        }
    }
    private void OnTriggerStay2D(Collider2D gC)
    {
        jumping = false;
    }
    private void OnTriggerExit2D(Collider2D gC)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            jumpSound.Play();
        }
    }
}
