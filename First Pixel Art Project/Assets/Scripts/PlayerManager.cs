using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Vector2 playerPos;
    public Animator animator;
    public AudioSource walkSound;
    public AudioSource jumpSound1;
    public AudioSource jumpSound2;
    public GameObject groundCheck;
    public Rigidbody2D rB;
    public SpriteRenderer sR;
    public BoxCollider2D gC;


    public float speed = 1f;
    [SerializeField] public float normalSpeed = 2f;
    [SerializeField] public float sprintSpeed = 4f;
    [SerializeField] public float jumpForce = 1.5f;
    [SerializeField] public float maxRotation = 2f;
    [SerializeField] public bool sprint = false;
    [SerializeField] public bool jumping = false;
    [SerializeField] public float jumpTime;

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
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !jumping && this.tag == "Player2")
        {
            Jump();
            jumping = true;
            animator.SetBool("jumping", true);
            Vector2 force = Vector2.up;
            force.y += (jumpForce - 20) * Time.deltaTime;
            rB.AddForce(force, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.W) && !jumping && this.tag == "Player1")
        {
            Jump();
            jumping = true;
            animator.SetBool("jumping", true);
            Vector2 force = Vector2.up;
            force.y += (jumpForce - 20) * Time.deltaTime;
            rB.AddForce(force, ForceMode2D.Impulse);
        }
    }
    private void InputCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && this.tag == "Player2")
        {
            sR.flipX = true;
            animator.SetBool("walking", true);
            playerPos = transform.position;
            playerPos.x -= speed * Time.deltaTime;
            transform.position = playerPos;
        }
        if (Input.GetKey(KeyCode.A) && this.tag == "Player1")
        {
            sR.flipX = true;
            animator.SetBool("walking", true);
            playerPos = transform.position;
            playerPos.x -= speed * Time.deltaTime;
            transform.position = playerPos;
        }
        if (Input.GetKey(KeyCode.RightArrow) && this.tag == "Player2")
        {
            sR.flipX = false;
            animator.SetBool("walking", true);
            playerPos = transform.position;
            playerPos.x += speed * Time.deltaTime;
            transform.position = playerPos;
        }
        if (Input.GetKey(KeyCode.D) && this.tag == "Player1")
        {
            sR.flipX = false;
            animator.SetBool("walking", true);
            playerPos = transform.position;
            playerPos.x += speed * Time.deltaTime;
            transform.position = playerPos;
        }
    }
    public void SprintCheck()
    {
        if (Input.GetKey(KeyCode.RightShift) && this.tag == "Player2")
        {
            speed = sprintSpeed;
        }
        if (Input.GetKey(KeyCode.LeftShift) && this.tag == "Player1")
        {
            speed = sprintSpeed;
        }
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            speed = normalSpeed;
        }
    }

    public void AngelCheck(float zRotation)
    {
        if (zRotation > maxRotation || zRotation < -maxRotation)
        {
            rB.angularVelocity = 0f;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        }
    }

    public void Step()
    {
        walkSound.Play();
    }

    public void Jump()
    {
        System.Random rnd = new System.Random();
        int temp = rnd.Next(0, 2);
        if (temp == 0) jumpSound1.Play();
        if (temp == 1) jumpSound2.Play();
    }

    private void OnTriggerStay2D(Collider2D gC)
    {
        Debug.Log("Jumping false " + this);
        jumping = false;
    }

    private void OnTriggerExit2D(Collider2D gC)
    {
        animator.SetBool("jumping", false);
    }
}