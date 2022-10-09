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
                if(waterObtained == true) {
                    triggerActive = true;
                    fireIcon.SetActive(true);
                    Debug.Log("Press O to put out the fire");
                }
            }
        }
 
        public void OnTriggerExit(Collider fire)
        {
            if (fire.CompareTag("Player"))
            {
                triggerActive = false;
                fireIcon.SetActive(false);
            }
        }
 
        private void Update()
        {
            //Check if player has water before they are able to put out the object
            waterObtained = player.GetComponent<Player>().WaterObtainedValue();

            //Keyboard Action
            if (triggerActive && Input.GetKeyDown(KeyCode.O))
            {
                Interact();
            }
        }
 
        public void Interact()
        {
            //For controller input
            if (triggerActive) {
                fireObjectItem.SetActive(false);
                player.GetComponent<Player>().PlayerNoBucket();
                Debug.Log("YAY YOU HAVE PUT OUT THE FIRE");
            }
        }
}
