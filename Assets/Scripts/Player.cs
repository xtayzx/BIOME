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
    private bool oilCleanerObtained = false;

    private bool triggerActiveWater = false;

    private bool jumpKeyPressed;
    private float horizontalInput;
    [SerializeField] private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining;
    
    private float landSpeed = 2f;
    private float waterSpeed = 1f;

    private float speed;
    // private float verticalSpeed = 1.5f;

    public float fallingThreshold = -6f;
    [HideInInspector]
    private bool falling = false;
    private float fallPoint = -5f;

    GameManager gameManager;

    public GameObject playerBucketIcon;
    public GameObject playersBucket;
    // private bool isGrounded;

    PlayerControls controls;
    // Vector2 move;
    //Left Joystick reads as Vector2 but our 3D world is a Vector3

    //////

    private Vector3 input;
    private float turnSpeed = 360;

    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
    private Item selectedItem;
    [SerializeField] public Item bucket;
    [SerializeField] public Item filledBucket;
    [SerializeField] public Item apple;
    [SerializeField] public Item oilCleaner;
    [SerializeField] public Item duckItem;

    private int inventoryApples = 0;
    private bool inWater = true; // Although not in the water to start, this is so it doesn't trigger upon the game loading

    // INVENTORY KEY
    // 0 - Empty Bucket
    // 1 - Filled Bucket
    // 2 - Apple
    // 3 - Oil Cleaner
    // 4 - Duck Item


    void Awake () {
        //CONTROLLER FUNCTIONALITY
        controls = new PlayerControls();
        // gameManager.ActivateControls("PlayControls");

        controls.Gameplay.Jump.performed += ctx => Jump();

        controls.Gameplay.Interact.performed += ctx => Interact();

        //set move to the value of our thumbstick
        // controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();

        //set to zero when not moving
        // controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        checkpointPosition = this.transform.position;
        speed = landSpeed; //Set the speed of the player
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
    // void Start()
    // {
        // Set game frame rate - cause my fans are going crazy so I think this sets it up
        // TODO: Delete later as already called in Main Menu
        // Application.targetFrameRate = 90;
        
        // rigidbodyComponent = GetComponent<Rigidbody>();
    // }

    void GatherInput() {
        input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
    }

    void Move() {
        rigidbodyComponent.MovePosition(transform.position + (transform.forward * input.magnitude) * speed * Time.deltaTime);
    }

    void Look() {

        if (input != Vector3.zero) {

            var relative = (transform.position + input.ToIso()) - transform.position; //find relative angle between us
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        Look();
        selectedItem = inventoryManager.GetSelectedItem();

        //JUMP (space bar pressed)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
        }

        //GETTING WATER
        if (Input.GetKey(KeyCode.E))
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
        if (triggerActiveWater == true && waterObtained == false) {
            FindObjectOfType<AudioManager>().Play("Splash");
            // waterObtained = true;
            UseSelectedItem();
            // Debug.Log("Bucket is removed");
            
            // Bucket(0);
            
            
            PickupItem(1);
            // Debug.Log("Filled bucket is added");
            // Debug.Log("Player has obtained water");
            // playersBucket.SetActive(true);
            waterObtained = true;
            return;
            //TODO: add code here to make it look like the bucket has water now 
        }
    }

    public void Bucket(int num) {
        
        //FILL BUCKET
        if(num == 1) {
            UseSelectedItem();
            PickupItem(1);
            return;
        }

        //EMPTY BUCKET
        else if (num == 0) {
            UseSelectedItem();
            PickupItem(0);
            return;
        }
        
    }

    public void CollectInventory(int num) {
        // UseSelectedItem();
        if (num == 2) {
            PickupItem(2);
            inventoryApples++;
            Debug.Log("Number of apples: "+inventoryApples);
        }

        else if (num == 3) {
            PickupItem(3);
            oilCleanerObtained = true;
            Debug.Log("Picked up oil cleaner");
            Debug.Log("Oil Cleaner Obtained: "+oilCleanerObtained);
        }

        else if (num == 4) {
            PickupItem(4);
            Debug.Log("Picked up Duck item");
        }
        
    }

    public Item playerSelectedItem() {
        return selectedItem;
    }

    public Item PlayerSelectedFilledBucket() {
        return filledBucket;
    }

    public Item PlayerSelectedOilCleaner() {
        return oilCleaner;
    }


    // JUMPING MECHANICS
    //fixedUpdate called once every physics update
    void FixedUpdate() {
        Move();
        // rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

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

    // INVENTORY
    public void PickupItem(int id) {
       bool result = inventoryManager.AddItem(itemsToPickup[id]);
       if (result == true) {
            Debug.Log("Item added");
       }
       else {
            Debug.Log("Item not added");
       }
    }

    public void UseSelectedItem() {
       Item receivedItem = inventoryManager.UseItem(true);
       if (receivedItem != null) {
            Debug.Log("Used item: " + receivedItem);
       }
       else {
            Debug.Log("No item used");
       }
    }
    

    //COLLECT BUCKET

    public void ActiveBucket(int number) {

        if(number == 1) {
            bucketObtained = true;
            // Debug.Log("Bucket has essentially been collected");
            PickupItem(0);
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

    public bool OilCleanerObtainedValue() {
        return oilCleanerObtained;
    }

    public void PlayerNoBucket() {
        bucketFilled = false;
        waterObtained = false;
        playersBucket.SetActive(false);
    }

    public void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Water") && selectedItem == bucket)
        {
            if (bucketObtained == true) {
                FindObjectOfType<AudioManager>().Play("Object");
                triggerActiveWater = true;
                playerBucketIcon.SetActive(true);
                if(FindObjectOfType<GameManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().ShowControls();
                }
                // Debug.Log("Press O to fill bucket");
            }
        }

        if (player.CompareTag("Water") && selectedItem != bucket && bucketObtained == true && bucketFilled == false) {
            if (FindObjectOfType<GameManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial3>().ShowInventoryControls();
                FindObjectOfType<Tutorial4>().ShowInventoryControls();
            }
        }

        if (player.CompareTag("TutorialJump"))
        {
            if(FindObjectOfType<GameManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial2>().ShowJump();
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

                if(FindObjectOfType<GameManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().HideControls();
                    FindObjectOfType<Tutorial3>().HideInventoryControls();
                    FindObjectOfType<Tutorial4>().HideInventoryControls();
                }
            
            }

            else if (bucketObtained == false) {
                if(FindObjectOfType<GameManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial3>().HideInventoryControls();
                    FindObjectOfType<Tutorial4>().HideInventoryControls();
                }
            }
        }

        if (player.CompareTag("TutorialJump"))
        {
            if(FindObjectOfType<GameManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial2>().HideJump();
            }
        }
    }

    public void OnCollisionEnter(Collision collision) {
        //WATER LAYER
        if (collision.gameObject.layer == 3) {
            if(inWater == true) {
            FindObjectOfType<AudioManager>().Play("Splash");
            Debug.Log("Entered Water");
            speed = waterSpeed;
            inWater = false;
            return;
            }
        }
        // isGrounded = true;
    }

    public void OnCollisionExit(Collision collision) {
        // isGrounded = false;

        // WATER LAYER
        if (collision.gameObject.layer != 3 && inWater == false) {
            FindObjectOfType<AudioManager>().Play("Splash");
            Debug.Log("Exited water");
            speed = landSpeed;
            inWater = true;
        }
    }
}

