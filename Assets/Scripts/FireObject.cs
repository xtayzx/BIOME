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

    GameManager gameManager;

    PlayerControls controls;

        void Awake() {
            controls = new PlayerControls();

            // gameManager.ActivateControls("PlayControls");
            controls.Gameplay.Interact.performed += ctx => Interact();
            // controls.Gameplay.Conversation.performed += ctx => Conversation();
        }

        void OnEnable() {
            controls.Gameplay.Enable();
        }

        void OnDisable() {
            controls.Gameplay.Disable();
        }
 
        public void OnTriggerEnter(Collider fire)
        {
            if (fire.CompareTag("Player"))
            {
                if(waterObtained == true && player.GetComponent<Player>().playerSelectedItem() == player.GetComponent<Player>().playerSelectedFilledBucket()) {
                    FindObjectOfType<AudioManager>().Play("Object");
                    triggerActive = true;
                    fireIcon.SetActive(true);
                    Debug.Log("Press O to put out the fire");

                    if(FindObjectOfType<GameManager>().Tutorial() == true) {
                        FindObjectOfType<Tutorial>().ShowControls();
                    }

                }

                if(waterObtained == true && player.GetComponent<Player>().playerSelectedItem() != player.GetComponent<Player>().playerSelectedFilledBucket()) {
                        if(FindObjectOfType<GameManager>().Tutorial() == true) {
                            FindObjectOfType<Tutorial3>().ShowInventoryControls();
                         }
                }
            }
        }
 
        public void OnTriggerExit(Collider fire)
        {
            if (fire.CompareTag("Player"))
            {
                triggerActive = false;
                fireIcon.SetActive(false);

                if(FindObjectOfType<GameManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().HideControls();
                    FindObjectOfType<Tutorial3>().HideInventoryControls();
                }
            }
        }
 
        private void Update()
        {
            //Check if player has water before they are able to put out the object
            waterObtained = player.GetComponent<Player>().WaterObtainedValue();

            //Keyboard Action
            if (triggerActive && Input.GetKeyDown(KeyCode.E))
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
                // Debug.Log("YAY YOU HAVE PUT OUT THE FIRE");

                if(FindObjectOfType<GameManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().HideControls();
                    FindObjectOfType<Tutorial3>().HideInventoryControls();
                }
            }
        }
}
