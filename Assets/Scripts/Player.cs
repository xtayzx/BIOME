using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private bool startConvo = true;

    private bool bucketObtained = false;
    private bool bucketFilled = false;
    private bool waterObtained = false;

    private bool triggerActiveWater = false;

    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining;
    private float speed = 1f;
    private float verticalSpeed = 1.5f;

    GameManager gameManager;

    public GameObject playerBucketIcon;
    public GameObject playersBucket;
    // private bool isGrounded;

    PlayerControls controls;
    Vector2 move;
    //Left Joystick reads as Vector2 but our 3D world is a Vector3

    void Awake () {
        //CONTROLLER FUNCTIONALITY
        controls = new PlayerControls();
        // gameManager.ActivateControls("PlayControls");

        controls.Gameplay.Jump.performed += ctx => Jump();

        controls.Gameplay.Interact.performed += ctx => Interact();

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
        Vector3 moveWE = new Vector3(move.x, 0, 0) * Time.deltaTime*speed;
        Vector3 moveNS = new Vector3(0, 0, move.y) * Time.deltaTime*verticalSpeed;
        transform.Translate((moveNS+moveWE), Space.World);
        
        //***KEYBOARD FUNCTIONALITY
        //FORWARD
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.forward*Time.deltaTime*verticalSpeed);
        }

        //BACK
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.back*Time.deltaTime*verticalSpeed);
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

        //GETTING WATER
        if (Input.GetKey(KeyCode.O))
        {
            Interact();
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    public bool startConvoActive() {
        return startConvo;
    }

    public void startConvoValue(int value) {
            if(value == 0) {
                startConvo = false;
            }

            if(value == 1) {
                startConvo = true;
            }
        }

    private void Interact() {
        if (triggerActiveWater == true) {
            waterObtained = true;
            Debug.Log("Player has obtained water");
            playersBucket.SetActive(true);
            //TODO: add code here to make it look like the bucket has water now 
        }
    }

    //fixedUpdate called once every physics update
    private void FixedUpdate() {

        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

        //always colliding with self
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
            return;
        }

        // Check if space key is pressed down
        if (jumpKeyPressed)
        {
            float jumpPower = 5f;
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

        
    }

    public void ActiveBucket(int number) {

        if(number == 1) {
            bucketObtained = true;
            Debug.Log("Bucket has essentially been collected");
            //TODO: code here to show that player has a bucket
        }

        else if (number == 0) {
            bucketObtained = false;
        }

    }

    //WATER DYNAMICS

    public bool BucketObtainedValue() {
        return bucketObtained;
    }

    public bool BucketFilledValue() {
        return bucketFilled;
    }

    public bool WaterObtainedValue() {
        return waterObtained;
    }

    public void PlayerNoBucket() {
        bucketFilled = false;
        waterObtained = false;
        playersBucket.SetActive(false);
    }

    public void OnTriggerEnter(Collider player)
        {
            if (player.CompareTag("Water"))
            {
                if (bucketObtained == true) {
                triggerActiveWater = true;
                playerBucketIcon.SetActive(true);
                Debug.Log("Press O to fill bucket");
                }
            }
        }
 
    public void OnTriggerExit(Collider player)
        {
            if (player.CompareTag("Water"))
            {
                if (bucketObtained == true) {
                triggerActiveWater = false;
                playerBucketIcon.SetActive(false);
                }
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

