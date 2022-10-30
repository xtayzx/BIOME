using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Apple : MonoBehaviour
{
   [SerializeField] private bool triggerActive = false;

    public GameObject AppleObject;
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
 
        public void OnTriggerEnter(Collider apple)
        {
            if (apple.CompareTag("Player"))
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
 
        public void OnTriggerExit(Collider apple)
        {
            if (apple.CompareTag("Player"))
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
                AppleObject.SetActive(false);
                Player.GetComponent<Player>().Apple();
            }
        }
}
