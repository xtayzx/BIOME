using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FireObject : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    public GameObject fireObjectItem;
    public GameObject fireIcon;
    public GameObject player;

    private bool waterObtained;

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

    //When the player enters the boundaries, then allow them to interact with the object
    public void OnTriggerEnter(Collider fire)
    {
        if (fire.CompareTag("Player"))
        {
            // If the player has water in the bucket and the player has selected the bucket of water in their inventory, enable the trigger
            if(waterObtained == true && player.GetComponent<Player>().playerSelectedItem() == player.GetComponent<Player>().PlayerSelectedFilledBucket()) {
                FindObjectOfType<AudioManager>().Play("Object");
                triggerActive = true;
                fireIcon.SetActive(true);

                if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().ShowControls();
                }
                FindObjectOfType<Player>().WithinCollider(true);
            }

            
            // If the player has water in the bucket and the player does not have the bucket of water selected in their inventory, if tutorial then show UI visuals
            if(waterObtained == true && player.GetComponent<Player>().playerSelectedItem() != player.GetComponent<Player>().PlayerSelectedFilledBucket()) {
                    if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                        FindObjectOfType<Tutorial3>().ShowInventoryControls();
                        FindObjectOfType<Tutorial4>().ShowInventoryControls();
                    }
                FindObjectOfType<Player>().WithinCollider(true);
            }
        }
    }

    //When the player exits the boundaries, turn off UI visuals
    public void OnTriggerExit(Collider fire)
    {
        if (fire.CompareTag("Player"))
        {
            triggerActive = false;
            fireIcon.SetActive(false);
            FindObjectOfType<Player>().WithinCollider(false);

            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().HideControls();
                FindObjectOfType<Tutorial3>().HideInventoryControls();
                FindObjectOfType<Tutorial4>().HideInventoryControls();
            }
        }
    }

    private void Update()
    {
        //Check if player has water before they are able to put out the object
        waterObtained = player.GetComponent<Player>().WaterObtainedValue();

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
            FindObjectOfType<AudioManager>().Play("Splash");
            fireObjectItem.SetActive(false);
            player.GetComponent<Player>().PlayerNoBucket();
            player.GetComponent<Player>().Bucket(0);
            FindObjectOfType<Player>().WithinCollider(false);
            
            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().HideControls();
                FindObjectOfType<Tutorial3>().HideInventoryControls(); 
            }
        }
    }
}
