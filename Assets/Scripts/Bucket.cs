using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Bucket : MonoBehaviour
{
   [SerializeField] private bool triggerActive = false;

    public GameObject BucketObject;
    public GameObject BucketIcon;
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
 
        public void OnTriggerEnter(Collider bucket)
        {
            if (bucket.CompareTag("Player"))
            {
                triggerActive = true;
                BucketIcon.SetActive(true);
                Debug.Log("Press O to collect the bucket");
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
            if (triggerActive && Input.GetKeyDown(KeyCode.O))
            {
                Interact();
            }
        }
 
        public void Interact()
        {
            //For controller input
            if (triggerActive) {
                BucketObject.SetActive(false);
                Player.GetComponent<Player>().ActiveBucket(1);
            }
        }
}