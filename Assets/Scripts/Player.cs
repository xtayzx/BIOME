using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining;
    private float speed = 1f;
    // private bool isGrounded;

    PlayerControls controls;
    Vector2 move;
    //Left Joystick reads as Vector2 but our 3D world is a Vector3

    void Awake () {
        //CONTROLLER FUNCTIONALITY
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => Jump();

        //set move to the value of our thumbstick
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();

        //set to zero when not moving
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    void Jump() {
        jumpKeyPressed = true;
        // Debug.Log("Jump pressed");
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set game frame rate - cause my fans are going crazy so I think this sets it up
        // TODO: Delete later as already called in Main Menu
        Application.targetFrameRate = 90;
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //***CONTROLLER FUNCTIONALITY
        Vector3 moveAction = new Vector3(move.x, 0, move.y) * Time.deltaTime*speed;
        transform.Translate(moveAction, Space.World);
        
        //***KEYBOARD FUNCTIONALITY
        //FORWARD
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.forward*Time.deltaTime*speed);
        }

        //BACK
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.back*Time.deltaTime*speed);
        }

        //LEFT
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector3.left*Time.deltaTime*speed);
        }

        //RIGHT
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right*Time.deltaTime*speed);
        }

        //JUMP (space bar pressed)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    //fixedUpdate called once every physics update
    private void FixedUpdate() {
        //no air jump
        // if (!isGrounded) {
        //     return;
        // }

        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

        //always colliding with self
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
            return;
        }

        // Check if space key is pressed down
        if (jumpKeyPressed)
        {
            float jumpPower = 5f;
            // if (superJumpsRemaining > 0) {
            //     jumpPower *= 2;
            //     superJumpsRemaining--;
            // }
            
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

        
    }

    // private void OnCollisionEnter(Collision collision) {
    //     isGrounded = true;
    // }

    // private void OnCollisionExit(Collision collision) {
    //     isGrounded = false;
    // }

    // private void OnTriggerEnter(Collider other) {
    //     if (other.gameObject.layer == 9) {
    //         Destroy(other.gameObject);
    //         superJumpsRemaining++;
    //     }
    // }
}

