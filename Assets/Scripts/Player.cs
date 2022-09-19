using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //FORWARD
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(Vector3.forward*Time.deltaTime*speed);
        }

        //BACK
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(Vector3.back*Time.deltaTime*speed);
        }

        //LEFT
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(Vector3.left*Time.deltaTime);
        }

        //RIGHT
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(Vector3.right*Time.deltaTime);
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

