using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Bucket : MonoBehaviour
{
   [SerializeField] private bool triggerActive = false;

    public GameObject BucketObject; //Exact bucket object
    public GameObject BucketIcon; //Exact icon for bucket
    public GameObject Player; //Player in the level

    PlayerControls controls;

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

    public void OnTriggerEnter(Collider bucket)
    {
        if (bucket.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Object");
            triggerActive = true;
            BucketIcon.SetActive(true);
            
            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().ShowControls();
                FindObjectOfType<Player>().WithinCollider(true);
            }
        }
    }

    public void OnTriggerExit(Collider bucket)
    {
        if (bucket.CompareTag("Player"))
        {
            triggerActive = false;
            BucketIcon.SetActive(false);
            
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

    public void Interact()
    {
        //For controller input
        if (triggerActive) {
            FindObjectOfType<AudioManager>().Play("Interact");

            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().HideControls();
                FindObjectOfType<Player>().WithinCollider(false);
            }

            BucketObject.SetActive(false); //Remove bucket item from the level
            Player.GetComponent<Player>().ActiveBucket(1); //Return to player object that the bucket has been obtained
        }
    }
}
