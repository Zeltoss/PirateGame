using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // this script manages the movement of the player 
    // add a ground check and remove comment slashes to include jumping

    private PlayerControls _playerControls;
    private InputAction moveAction;
    // private InputAction jumpAction;

    private Rigidbody rb;

    private float speed = 0;
    private float maxSpeed = 12f;
    private float acceleration = 12f;
    private float stoppingForce = 18f;
    private Vector3 movement;
    private Vector3 lastMovement;
    private float movementX;
    private float movementY;
    // private float jumpingForce = 20f;

    public bool playerCanMove = true;
    // private bool isGrounded = true;


    void Awake()
    {
        _playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        moveAction = _playerControls.Player.Move;
        moveAction.Enable();

        /*
        jumpAction = _playerControls.Player.Jump;
        jumpAction.Enable();
        jumpAction.performed += Jump;
        */
    }

    private void OnDisable()
    {
        moveAction.Disable();
        // jumpAction.Disable();
    }


    private void Update()
    {
        if (playerCanMove)
        {
            movementX = moveAction.ReadValue<Vector2>().x;
            movementY = moveAction.ReadValue<Vector2>().y;
        }
        else
        {
            movement = Vector3.zero;
        }
    }


    void FixedUpdate()
    {
        movement = new Vector3(movementX, 0.0f, movementY);
        movement.Normalize();

        if (movement.sqrMagnitude > 0.0f)
        {
            Accelerate();
        }
        else if (movement.sqrMagnitude == 0.0f)
        {
            Decelerate();
        }
    }



    // this gives the movement a fade in
    private void Accelerate()
    {
        if (speed < maxSpeed)
        {
            speed += acceleration * Time.deltaTime;
        }
        else if (speed > maxSpeed)
        {
            speed -= stoppingForce * Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        rb.velocity = movement * speed;

        lastMovement = movement;
    }

    // this gives the movement a fade out
    private void Decelerate()
    {
        speed -= stoppingForce * Time.deltaTime;

        rb.velocity = lastMovement * speed;

        if (speed <= 0.0f)
        {
            speed = 0.0f;
            lastMovement = Vector3.zero;
        }
    }


    /*
    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    */
}
