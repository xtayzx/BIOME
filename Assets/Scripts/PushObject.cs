using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    public GameObject icon;
    public GameObject player;

    PlayerControls controls;
    private Vector3 objectPosition;
    Rigidbody rigidBody;
    Vector3 movementZ, movementX, movementZNeg, movementXNeg;
    RigidbodyConstraints originalConstraints;

    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Interact.performed += ctx => Interact();
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    void Start()
    {
        objectPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rigidBody = GetComponent<Rigidbody>();

        movementZ = new Vector3(0,0,5);
        movementZNeg = new Vector3(0,0,-5);
        movementX = new Vector3(5,0,0);
        movementXNeg = new Vector3(-5,0,0);

        originalConstraints = rigidBody.constraints;
    }

    //Sound when entering water
    public void OnCollisionEnter(Collision collision) {
        //WATER LAYER
        if (collision.gameObject.layer == 3) {
            FindObjectOfType<AudioManager>().Play("Splash");
            Debug.Log("Rock entered Water");
            return;
        }
    }

    //When the player enters the boundaries, then allow them to interact with the object
    public void OnTriggerEnter(Collider rock)
    {
        if (rock.CompareTag("Player"))
        {
            // If the player has water in the bucket and the player has selected the bucket of water in their inventory, enable the trigger
                triggerActive = true;
                icon.SetActive(true);
                FindObjectOfType<Player>().WithinCollider(true);

                if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().ShowControls();
                    
                }
        }
    }

    //When the player exits the boundaries, turn off UI visuals
    public void OnTriggerExit(Collider rock)
    {
        if (rock.CompareTag("Player"))
        {
            triggerActive = false;
            icon.SetActive(false);
            FindObjectOfType<Player>().WithinCollider(false);

            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().HideControls();
                
            }
        }
    }

    private void Update()
    {
        //Keyboard Action
        if (triggerActive && Input.GetKeyDown(KeyCode.J))
        {
            Interact();
        }
    }

    void FixedUpdate() {
        if(rigidBody.velocity == new Vector3(0,0,0)) {
            rigidBody.constraints = originalConstraints;
        }
    }

    public void Interact()
    {
        //For controller input
        if (triggerActive) {
            rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            Debug.Log("PLAYER POS X: "+player.transform.position.x+" /// BLOCK POS X: "+transform.position.x+" /// PLAYER POS Z: "+player.transform.position.z+" /// BLOCK POS Z: "+transform.position.z);

            if(player.transform.position.x < transform.position.x && (player.transform.position.z < (transform.position.z+0.5)) && (player.transform.position.z > (transform.position.z-0.5))) {
                rigidBody.velocity = movementX;
                Debug.Log("Move block east");
                FindObjectOfType<AudioManager>().Play("RockPush");
                return;
            }

            else if(player.transform.position.x > transform.position.x && (player.transform.position.z < (transform.position.z+0.5)) && (player.transform.position.z > (transform.position.z-0.5))) {
                rigidBody.velocity = movementXNeg;
                Debug.Log("Move block west");
                FindObjectOfType<AudioManager>().Play("RockPush");
                return;
            }

            else if(player.transform.position.z < transform.position.z && (player.transform.position.x < (transform.position.x+0.5)) && (player.transform.position.x > (transform.position.x-0.5))) {
                rigidBody.velocity = movementZ;
                Debug.Log("Move block up");
                FindObjectOfType<AudioManager>().Play("RockPush");
                return;
            }

            else if(player.transform.position.z > transform.position.z && (player.transform.position.x < (transform.position.x+0.5)) && (player.transform.position.x > (transform.position.x-0.5))) {
                rigidBody.velocity = movementZNeg;
                Debug.Log("Move block down");
                FindObjectOfType<AudioManager>().Play("RockPush");
                return;
            }
            
        }
    
        
    }

    // Resetting the object when the game is reset to the previous checkpoint
    public void ObjectStartPosition() {
        this.transform.position = objectPosition;
        this.transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().Sleep();
    }

}
