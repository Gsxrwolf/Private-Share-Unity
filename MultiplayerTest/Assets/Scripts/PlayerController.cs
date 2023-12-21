using System;
using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Connection;
using FishNet.Object;

public class PlayerController : NetworkBehaviour
{
    Camera cam;
    Rigidbody rb;

    [SerializeField] float speed = 10;
    [SerializeField] float sprintSpeedToAdd = 5;
    [SerializeField] float sprintTime = 10;
    [SerializeField] float sprintTimeIncrease = 1;
    [SerializeField] float sprintTimeDecrease = 2;
    float curSprintTime;
    bool onCooldown = false;

    [SerializeField] private int health = 3;

    [SerializeField] float camYOffset;
    [SerializeField] float sensiX = 0.1f;
    [SerializeField] float sensiY = 0.1f;
    [SerializeField] int clampValue = 225;



    Vector2 moveInput;
    Vector3 direction;
    Vector2 mouseInput;
    Vector3 rotationVecPlayer;
    Vector3 rotationVecCam;
    float sprintInput;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            cam = Camera.main;
            cam.transform.position = new Vector3 (transform.position.x, transform.position.y + camYOffset, transform.position.z);
            cam.transform.SetParent(transform);
        }
        else
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
        }

    }


    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if (health == 0)
        {
            return;
        }
        if (curSprintTime < sprintTime && sprintInput == 1 && !onCooldown)
        {
            curSprintTime += sprintTimeIncrease * Time.deltaTime;
        }
        else
        {
            onCooldown = true;
            sprintInput = 0;
            curSprintTime -= sprintTimeDecrease * Time.deltaTime;
        }

        if (curSprintTime < 0)
        {
            curSprintTime = 0;
            onCooldown = false;
        }


        direction = moveInput.x * transform.right + moveInput.y * transform.forward;
        rb.AddForce(direction * (speed + (sprintSpeedToAdd * sprintInput)));
        transform.rotation = Quaternion.Euler(rotationVecPlayer * sensiX);
        cam.transform.rotation = Quaternion.Euler(new Vector3(rotationVecCam.x, rotationVecPlayer.y, 0) * sensiY);
    }

    private void OnDeath()
    {

    }

    public int getHealth()
    {
        return health;
    }
    public void dealDamage(int _damage)
    {
        health -= _damage;
        if (health < 0)
        {
            OnDeath();
            health = 0;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext _input)
    {
        moveInput = _input.ReadValue<Vector2>();
    }
    public void OnSprintInput(InputAction.CallbackContext _input)
    {
        sprintInput = _input.ReadValue<float>();
    }

    public void OnMouseInput(InputAction.CallbackContext _input)
    {
        mouseInput = _input.ReadValue<Vector2>();
        rotationVecCam.x -= mouseInput.y;
        rotationVecCam.x = Mathf.Clamp(rotationVecCam.x, -clampValue, clampValue);
        rotationVecPlayer.y += mouseInput.x;
    }
}
