using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private Vector3 checkpointPosition;

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

    public float fallingThreshold = -6f;
    [HideInInspector]
    private bool falling = false;
    private float fallPoint = -20f;

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

        checkpointPosition = this.transform.position;
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
        // Application.targetFrameRate = 90;
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveN = new Vector3(1, 0, 0) * Time.deltaTime*speed;
        Vector3 moveW = new Vector3(0, 0, -1) * Time.deltaTime*speed;
        Vector3 moveS = new Vector3(-1, 0, 0) * Time.deltaTime*speed;
        Vector3 moveE = new Vector3(0, 0, 1) * Time.deltaTime*speed;
        Vector3 rotation = new Vector3(0, 0, 1);

        // Vector3 rotation = new Vector3(0, 0, Camera.main.transform.localEulerAngles.y) * Time.deltaTime*speed;

        Vector3 newN = moveN+moveE;
        Vector3 newS = moveS+moveW;
        Vector3 newW = (moveW+rotation);
        Vector3 newE = (moveE+rotation);
        // Debug.Log("ROTATION VALUE: "+ Camera.main.transform.localEulerAngles.y);

        //***CONTROLLER FUNCTIONALITY
        Vector3 moveWE = new Vector3(move.x, 0, 0) * Time.deltaTime*speed;
        Vector3 moveNS = new Vector3(0, 0, move.y) * Time.deltaTime*verticalSpeed;
        transform.Translate((moveNS+moveWE), Space.World);
        
        //***KEYBOARD FUNCTIONALITY
        //FORWARD
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            // WORLD DIRECTION
            // transform.Translate(Vector3.forward*Time.deltaTime*verticalSpeed);

            transform.Translate(newN, Space.World);
        }

        //BACK
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            // WORLD DIRECTION
            // transform.Translate(Vector3.back*Time.deltaTime*verticalSpeed);

            transform.Translate(newS, Space.World);
        }

        //LEFT
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            // WORLD DIRECTION
            // transform.Translate(Vector3.left*Time.deltaTime*speed);

            transform.Translate((Vector3.left+rotation)*Time.deltaTime);
            // transform.Translate(direction*Time.deltaTime);
            // transform.Translate(desiredMoveDirection*Time.deltaTime);
        }

        //RIGHT
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            // WORLD DIRECTION
            // transform.Translate(Vector3.right*Time.deltaTime*speed);
            
            // transform.Translate(desiredMoveDirection*Time.deltaTime);
            transform.Translate((Vector3.right-rotation)*Time.deltaTime);
            // transform.Translate(rotation);
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

        // PLAYER FALL RESET
        if (this.transform.position.y < fallPoint) {
            FindObjectOfType<PauseMenu>().FallResetGame();
        }

        // PLAYER FALLING
        if (rigidbodyComponent.velocity.y < fallingThreshold) {
            falling = true;
            // Debug.Log("Player is falling");
        }

        else {
            falling = false;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    //CHECKPOINT
    public void CheckpointPositionChange(Vector3 newPosition) {
        checkpointPosition = newPosition;
    }

    public void StartAtCheckpoint() {
        this.transform.position = checkpointPosition;
    }

    //INTERACTING WITH OBJECTS AND NPC
    public bool startConvoActive() {
        return startConvo;
    }

    public bool playerIsFalling() {
        return falling;
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
            FindObjectOfType<AudioManager>().Play("Splash");
            waterObtained = true;
            Debug.Log("Player has obtained water");
            playersBucket.SetActive(true);
            //TODO: add code here to make it look like the bucket has water now 
        }
    }


    // JUMPING MECHANICS
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
            FindObjectOfType<AudioManager>().Play("Jump");
            float jumpPower = 5f;
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

        

        // Debug.Log("Player falling: " + falling);
    }

    //COLLECT BUCKET

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
                FindObjectOfType<AudioManager>().Play("Object");
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

