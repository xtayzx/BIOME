using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Oil : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    public GameObject oilObjectItem;
    public GameObject oilIcon;
    public GameObject player;
    private bool cleanerObtained = false;

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
 
        public void OnTriggerEnter(Collider oil)
        {
            if (oil.CompareTag("Player"))
            {
                Debug.Log("Colliding with player");
                if(cleanerObtained == true && player.GetComponent<Player>().playerSelectedItem() == player.GetComponent<Player>().PlayerSelectedOilCleaner()) {
                    FindObjectOfType<AudioManager>().Play("Object");
                    triggerActive = true;
                    oilIcon.SetActive(true);

                    if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                        FindObjectOfType<Tutorial>().ShowControls();
                    }

                }

                if(cleanerObtained == true && player.GetComponent<Player>().playerSelectedItem() != player.GetComponent<Player>().PlayerSelectedOilCleaner()) {
                        // if(FindObjectOfType<GameManager>().Tutorial() == true) {
                        //     FindObjectOfType<Tutorial3>().ShowInventoryControls();
                        //  }
                    Debug.Log("Oil cleaner is not selected in inventory");
                }
            }
        }
 
        public void OnTriggerExit(Collider oil)
        {
            if (oil.CompareTag("Player"))
            {
                triggerActive = false;
                oilIcon.SetActive(false);

                if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().HideControls();
                    FindObjectOfType<Tutorial3>().HideInventoryControls();
                }
            }
        }
 
        private void Update()
        {
            //Check if player has water before they are able to put out the object
            cleanerObtained = player.GetComponent<Player>().OilCleanerObtainedValue();

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
                FindObjectOfType<AudioManager>().Play("OilClean");
                oilObjectItem.SetActive(false);

                if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().HideControls();
                    FindObjectOfType<Tutorial3>().HideInventoryControls();
                }
            }
        }
}
