using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    private UnityEngine.Vector3 movement;
    private UnityEngine.Vector3 lastMovement;
    private float movementX;
    private float movementY;
    // private float jumpingForce = 20f;

    public bool playerCanMove = true;
    // private bool isGrounded = true;

    private bool facingLeft = true;
    [SerializeField] private GameObject playerSprite;
    private GameObject currentWeapon;



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

        PlayerAttack.onChangingWeapon += GetNewWeapon;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        // jumpAction.Disable();

        PlayerAttack.onChangingWeapon -= GetNewWeapon;
    }


    private void Update()
    {
        if (playerCanMove)
        {
            movementX = moveAction.ReadValue<UnityEngine.Vector2>().x;
            movementY = moveAction.ReadValue<UnityEngine.Vector2>().y;
        }
        else
        {
            movement = UnityEngine.Vector3.zero;
        }
    }


    void FixedUpdate()
    {
        movement = new UnityEngine.Vector3(movementX, 0.0f, movementY);
        movement.Normalize();

        if (movement.sqrMagnitude > 0.0f)
        {
            Accelerate();
        }
        else if (movement.sqrMagnitude == 0.0f)
        {
            Decelerate();
        }


        if (movement.x < 0 && !facingLeft)
        {
            FlipSprite();
        }
        else if (movement.x > 0 && facingLeft)
        {
            FlipSprite();
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
            lastMovement = UnityEngine.Vector3.zero;
        }
    }



    private void FlipSprite()
    {
        UnityEngine.Vector3 currentScale = playerSprite.transform.localScale;
        currentScale.x *= -1;
        playerSprite.transform.localScale = currentScale;

        UnityEngine.Vector3 weaponScale = currentWeapon.transform.localScale;
        weaponScale.x *= -1;
        currentWeapon.transform.localScale = weaponScale;
        UnityEngine.Vector3 currentPosition = currentWeapon.transform.localPosition;
        currentPosition.x *= -1;
        currentWeapon.transform.localPosition = currentPosition;
        UnityEngine.Quaternion currentRotation = currentWeapon.transform.localRotation;
        currentRotation.y *= -1;
        currentWeapon.transform.localRotation = currentRotation;

        facingLeft = !facingLeft;
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



    private void GetNewWeapon(GameObject weapon)
    {
        currentWeapon = weapon;
        if (!facingLeft)
        {
            UnityEngine.Vector3 weaponScale = currentWeapon.transform.localScale;
            weaponScale.x *= -1;
            currentWeapon.transform.localScale = weaponScale;
            UnityEngine.Vector3 currentPosition = currentWeapon.transform.localPosition;
            currentPosition.x *= -1;
            currentWeapon.transform.localPosition = currentPosition;
            UnityEngine.Quaternion currentRotation = currentWeapon.transform.localRotation;
            currentRotation.y *= -1;
            currentWeapon.transform.localRotation = currentRotation;
        }
    }
}
