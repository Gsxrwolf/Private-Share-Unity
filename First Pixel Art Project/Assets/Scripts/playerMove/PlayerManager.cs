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
    [SerializeField] public float normalSpeed = 1f;
    [SerializeField] public float sprintSpeed = 2f;
    [SerializeField] public float jumpForce = 1.5f;
    [SerializeField] public float maxRotation = 10f;
    [SerializeField] public bool sprint = false;
    [SerializeField] public bool jumping = false;
    [SerializeField] public float jumpTime;


    public void SprintCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
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

    public void OnTriggerStay2D(Collider2D gC)
    {
        jumping = false;

    }
    public void OnTriggerExit2D(Collider2D gC)
    {
        animator.SetBool("jumping", false);
    }
}
