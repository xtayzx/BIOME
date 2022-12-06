using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform; //Is the player on the ground
    [SerializeField] private LayerMask playerMask; //What can the player collide or not collide with
    
    private Vector3 checkpointPosition; //Setting the next checkpoint position
    private Vector3 levelHubStartPosition = new Vector3((float)-6, (float)7, (float)-6);

    private bool startConvo = true; //Is the player interacting with an NPC
    private bool bucketObtained = false; //Does the player have the bucket
    private bool bucketFilled = false; //Does the bucket contain water
    private bool waterObtained = false; //Has the player already obtained water
    private bool oilCleanerObtained = false; //Does the player have the oil cleaner
    private bool triggerActiveWater = false; //Is the trigger active when the player is near water

    private bool jumpKeyPressed; //Jump key pressed on space or controller
    [SerializeField] private Rigidbody rigidbodyComponent; //Set player RigidBody
    
    private float landSpeed = 3f; //Speed on land
    private float waterSpeed = 1f; //Speed in water
    private float speed; //The speed the player is moving at

    public float fallingThreshold = -6f; //Speed when player is falling
    [HideInInspector]
    private bool falling = false; //Is the player falling
    private float fallPoint = -5f; //Point at which the game resets to the nearest checkpoint

    GameManager gameManager;
    public GameObject playerBucketIcon; //Icon on player to fill bucket

    PlayerControls controls;
    public Animator laurelAnimation;

    private Vector3 input; //Input from controller
    private float turnSpeed = 360; //Turning the palyer

    // INVENTORY AND INVENTORY ITEMS
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
    private Item selectedItem;
    [SerializeField] public Item bucket;
    [SerializeField] public Item filledBucket;
    [SerializeField] public Item apple;
    [SerializeField] public Item oilCleaner;
    [SerializeField] public Item duckItem;
    [SerializeField] public Item trash;

    private int inventoryTrash = 0;
    private bool inWater = true; // Although not in the water to start, this is so it doesn't trigger upon the game loading
    private int selectedLevel;
    private bool showingUI = false; //For detecting if there's more colliders and preventing multiple jumps

    // INVENTORY KEY
    // 0 - Empty Bucket
    // 1 - Filled Bucket
    // 2 - Apple
    // 3 - Oil Cleaner
    // 4 - Duck Item
    // 5 - Trash Item

    void Awake () {
        //CONTROLLER FUNCTIONALITY
        controls = new PlayerControls();
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Interact.performed += ctx => Interact();

        checkpointPosition = this.transform.position; //To reset the checkpoint position when a checkpoint has been reached
        speed = landSpeed; //Set the speed of the player to begin

        if(FindObjectOfType<GameManager>().GetActiveLevel() == 0) {
            Debug.Log("Level Value: "+FindObjectOfType<GameManager>().CompletedLevelValue());
            if(FindObjectOfType<GameManager>().CompletedLevelValue() >= 1) {
                this.transform.position = levelHubStartPosition; //TODO: This position might need to be changed later
            }
        }
        laurelAnimation.SetBool("isWalking", false);
        laurelAnimation.SetBool("isIdle", true);
    }

    void Jump() {
        jumpKeyPressed = true;
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    // MOVEMENT
    void GatherInput() {
        input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
    }

    void Move() {
        if(startConvo == true){
            rigidbodyComponent.MovePosition(transform.position + (transform.forward * input.magnitude) * speed * Time.deltaTime);  
        }
    }

    void Look() {
        if(startConvo == true) {
            if (input != Vector3.zero) {
                var relative = (transform.position + input.ToIso()) - transform.position; //Find relative angle
                var rot = Quaternion.LookRotation(relative, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);

                // laurelAnimation.SetBool("isJumping", false);
                laurelAnimation.SetBool("isWalking", true);
                laurelAnimation.SetBool("isIdle", false);
                // FindObjectOfType<AudioManager>().Play("Walking");
            }

            else {
                // laurelAnimation.SetBool("isJumping", false);
                laurelAnimation.SetBool("isWalking", false);
                laurelAnimation.SetBool("isIdle", true);
                FindObjectOfType<AudioManager>().Play("Walking"); //Play walking sound
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput(); //Get axis
        Look(); //Rotate the player in another direction
        selectedItem = inventoryManager.GetSelectedItem(); //Return which item is selected in the inventory

        //JUMP (space bar pressed)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
        }

        //GETTING WATER
        if (Input.GetKey(KeyCode.J))
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
        }

        else {
            falling = false;
        }
    }

    //CHECKPOINT
    public void CheckpointPositionChange(Vector3 newPosition) {
        checkpointPosition = newPosition;
    }

    public void StartAtCheckpoint() {
        rigidbodyComponent.velocity = new Vector3(0,0,0);
        this.transform.position = checkpointPosition+new Vector3(1,(float)0.5,0);

        if(FindObjectOfType<GameManager>().GetActiveLevel() == 3) {
            
            //Remove ducks from inventory
            FindObjectOfType<Level3Ducks>().Level3Reset(true);
            inventoryManager.RemoveItem(duckItem);
        }
    }

    public Vector3 PlayerCheckpointPosition() {
        return checkpointPosition;
    }

    //INTERACTING WITH OBJECTS AND NPC
    public bool startConvoActive() {
        return startConvo;
    }

    public bool playerIsFalling() {
        return falling;
    }

    //Is the player interacting with an NPC
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
            UseSelectedItem();
            PickupItem(1);
            waterObtained = true;
            return;
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

    // FOR THE LEVEL HUB
    public void SelectedLevel(int num) {
        selectedLevel = num;
    }

    public int PlayerSelectedLevel() {
        return selectedLevel;
    }

    // For each item collected, this determines what to do next
    public void CollectInventory(int num) {
        if (num == 2) {
            PickupItem(2);
        }

        else if (num == 3) {
            PickupItem(3);
            oilCleanerObtained = true;
            Debug.Log("Picked up oil cleaner");
            Debug.Log("Oil Cleaner Obtained: "+oilCleanerObtained);
        }

        else if (num == 4) {
            PickupItem(4);
            FindObjectOfType<AudioManager>().Play("DuckQuack");
            Debug.Log("Picked up Duck item");
        }

        else if (num == 5) {
            PickupItem(5);
            inventoryTrash++;
            Debug.Log("Picked up TRASH");
            FindObjectOfType<LevelItems>().ShowItem(inventoryTrash); //For collecting items for Level 1 and getting a star
        }
    }

    public void IdleAnimation() {
        laurelAnimation.SetBool("isWalking", false);
        laurelAnimation.SetBool("isIdle", true);
        FindObjectOfType<AudioManager>().Pause("Walking");
    }

    // PLAYER SELECTED ITEMS
    public Item playerSelectedItem() {
        return selectedItem;
    }

    public Item PlayerSelectedFilledBucket() {
        return filledBucket;
    }

    public Item PlayerSelectedApple() {
        return apple;
    }

    public Item PlayerSelectedOilCleaner() {
        return oilCleaner;
    }

    public int TrashTotal() {
        return inventoryTrash;
    }


    // JUMPING MECHANICS
    // FixedUpdate called once every physics update
    void FixedUpdate() {
        Move();

        Debug.Log("Layers: "+Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length);
        
        //Since its always colliding with self
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
            return;
        }

        // Check if space key is pressed down
        if (jumpKeyPressed)
        {
            laurelAnimation.SetBool("isWalking", false);
            laurelAnimation.SetBool("isIdle", true);
            laurelAnimation.SetTrigger("triggerJump");
            FindObjectOfType<AudioManager>().Pause("Walking");
            

            float jumpPower = 5f;
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyPressed = false;
            FindObjectOfType<AudioManager>().Play("Jump");
        }
       
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
            PickupItem(0);
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
    }

    // CHECKING IF IN COLLISION WITH ANOTHER COLLIDER
    public void WithinCollider(bool state) {
        showingUI = state;
    }

    public bool ColliderStatus() {
        return showingUI;
    }

    public void OnTriggerEnter(Collider player)
    {
        //If the player is near water and they have the bucket, then enable they can interact with it
        if (player.CompareTag("Water") && selectedItem == bucket)
        {
            if (bucketObtained == true) {
                FindObjectOfType<AudioManager>().Play("Object");
                triggerActiveWater = true;
                playerBucketIcon.SetActive(true);
                showingUI = true;

                if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().ShowControls();
                }
            }
        }

        //If the player is near water, they do not have the bucket selected, but they have the bucket in their inventory and it's not filled, then show them they need to select the bucket in the inventory
        if (player.CompareTag("Water") && selectedItem != bucket && bucketObtained == true && bucketFilled == false) {
            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial3>().ShowInventoryControls();
                FindObjectOfType<Tutorial4>().ShowInventoryControls();
            }
            showingUI = true;
        }

        if (player.CompareTag("TutorialJump"))
        {
            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial2>().ShowJump();
                
            }
            showingUI = true;
        }
    }
 
    public void OnTriggerExit(Collider player)
    {
        if (player.CompareTag("Water"))
        {
            if (bucketObtained == true) {
                triggerActiveWater = false;
                playerBucketIcon.SetActive(false);

                if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().HideControls();
                    FindObjectOfType<Tutorial3>().HideInventoryControls();
                    FindObjectOfType<Tutorial4>().HideInventoryControls();
                    
                }
                showingUI = false;
            
            }

            else if (bucketObtained == false) {
                if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial3>().HideInventoryControls();
                    FindObjectOfType<Tutorial4>().HideInventoryControls();  
                }
                showingUI = false;
            }
        }

        if (player.CompareTag("TutorialJump"))
        {
            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial2>().HideJump();
            }
            showingUI = false;
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

    }

    public void OnCollisionExit(Collision collision) {

        // WATER LAYER
        if (collision.gameObject.layer != 3 && inWater == false) {
            FindObjectOfType<AudioManager>().Play("Splash");
            Debug.Log("Exited water");
            speed = landSpeed;
            inWater = true;
        }

    }
}