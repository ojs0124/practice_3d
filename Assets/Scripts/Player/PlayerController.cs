using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    private Vector2 moveInput;
    public float moveSpeed;

    [Header("Jump")]
    public LayerMask groundLayerMask;
    public float jumpPower;

    [Header("Look")]
    public Transform cameraContainer;
    private Vector2 mouseDelta;
    private float cameraRotateX;
    public float minRotateAngleX;
    public float maxRotateAngleX;
    public float sensitive;
    public bool canLook = true;

    public Action envSet;
    public Action inventory;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnAcceleration(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            moveSpeed *= 2;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            moveSpeed /= 2;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGround())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 moveVector = transform.forward * moveInput.y + transform.right * moveInput.x;
        moveVector *= moveSpeed;
        moveVector.y = _rigidbody.velocity.y;
        _rigidbody.velocity = moveVector;
    }

    void CameraLook()
    {
        cameraRotateX += mouseDelta.y * sensitive;
        cameraRotateX = Mathf.Clamp(cameraRotateX, minRotateAngleX, maxRotateAngleX);
        cameraContainer.localEulerAngles = new Vector3(-cameraRotateX, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * sensitive, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Trampoline")
        {
            _rigidbody.AddForce(Vector2.up * jumpPower * 3, ForceMode.Impulse);
        }
    }

    bool IsGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.1f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.1f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.1f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.1f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    public void OnEnvSet(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            envSet?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
