using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InteractiveObject : MonoBehaviour
{
[SerializeField] private bool triggerActive = false;
[SerializeField] private int itemNumber;

    public GameObject Object;
    public GameObject Icon;
    public GameObject Player;
    // private DialogueTrigger trigger;
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
 
        public void OnTriggerEnter(Collider item)
        {
            if (item.CompareTag("Player"))
            {
                FindObjectOfType<AudioManager>().Play("Object");
                triggerActive = true;
                Icon.SetActive(true);
                if(FindObjectOfType<GameManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().ShowControls();
                }
                // Debug.Log("Press O to collect the bucket");
            }
        }
 
        public void OnTriggerExit(Collider item)
        {
            if (item.CompareTag("Player"))
            {
                triggerActive = false;
                Icon.SetActive(false);
                if(FindObjectOfType<GameManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().HideControls();
                }
            }
        }
 
        private void Update()
        {
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
                FindObjectOfType<AudioManager>().Play("Interact");
                if(FindObjectOfType<GameManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().HideControls();
                }
                Object.SetActive(false);
                Player.GetComponent<Player>().CollectInventory(itemNumber);
            }
        }
}
