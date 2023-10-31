using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject GroundCheck;
    [SerializeField] float speed;
    [SerializeField] float jumpHight;
    [SerializeField] float sensi;
    [SerializeField] Vector2 moveInput;
    [SerializeField] Vector2 mouseInput;
    [SerializeField] Vector3 rotationVec;
    [SerializeField] Vector3 direction;
    [SerializeField] float jump;
    [SerializeField] bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction = moveInput.x * transform.right + moveInput.y * transform.forward;
        rb.AddForce(direction * speed);
        if (IsGrounded())
        {
            Vector3 jumpForce = new Vector3(0, jump * jumpHight, 0);
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
        transform.rotation = Quaternion.Euler(rotationVec * sensi);
    }
    public bool IsGrounded()
    {
        Vector3 shotPoint = new Vector3(transform.position.x, 0 - transform.localScale.y / 2, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            if (hit.collider.CompareTag("Floor"))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
        return isGrounded;
    }

    public void OnJumpInput(InputAction.CallbackContext _input)
    {
        jump = _input.ReadValue<float>();
    }
    public void OnMoveInput(InputAction.CallbackContext _input)
    {
        moveInput = _input.ReadValue<Vector2>();
    }

    public void OnMouseInput(InputAction.CallbackContext _input)
    {
        mouseInput = _input.ReadValue<Vector2>();
        rotationVec.y += mouseInput.x;
    }


}
