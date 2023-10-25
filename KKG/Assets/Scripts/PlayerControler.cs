using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody rB;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float camSpeedX = 1f;
    [SerializeField] private float camSpeedY = 1f;
    [SerializeField] private float camYBoarder = 1f;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 mouseInput;

    [SerializeField] private GameObject ankerPoint;

    private Vector2 rotationVec;
    private Vector3 direction;
    private float jump;
    // Start is called before the first frame update
    void Start()
    {
        //rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = moveInput.x * transform.right + moveInput.y * transform.forward;

        transform.rotation = Quaternion.Euler(0, rotationVec.x * camSpeedX, 0);
        rB.AddForce(0, jumpForce * jump, 0, ForceMode.Impulse);
    }
    private void LateUpdate()
    {
        ankerPoint.transform.rotation = Quaternion.Euler(rotationVec.y*camSpeedY, ankerPoint.transform.rotation.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        rB.AddForce(direction * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnMovementInput(InputAction.CallbackContext _input)
    {
        moveInput = _input.ReadValue<Vector2>();
    }
    public void OnMouseInput(InputAction.CallbackContext _input)
    {
        mouseInput = _input.ReadValue<Vector2>();
        rotationVec.x += mouseInput.x;
        if (rotationVec.y + mouseInput.y < camYBoarder && rotationVec.y + mouseInput.y > -camYBoarder)
        {
            rotationVec.y += mouseInput.y;
        }
    }
    public void OnJumpInput(InputAction.CallbackContext _input)
    {
        jump = _input.ReadValue<float>();
    }
}
