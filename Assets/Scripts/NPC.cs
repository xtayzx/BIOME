using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{

    [SerializeField] private bool triggerActive = false;

    public GameObject NPCObject;
    public GameObject NPCIcon;
    private DialogueTrigger trigger;
    GameManager gameManager;

    PlayerControls controls;

        void Awake() {
            controls = new PlayerControls();

            // gameManager.ActivateControls("PlayControls");
            controls.Gameplay.Talk.performed += ctx => Talk();
            controls.Gameplay.Conversation.performed += ctx => Conversation();
        }

        void OnEnable() {
            controls.Gameplay.Enable();
        }

        void OnDisable() {
            controls.Gameplay.Disable();
        }
 
        public void OnTriggerEnter(Collider npc)
        {
            if (npc.CompareTag("Player"))
            {
                triggerActive = true;
                NPCIcon.SetActive(true);
                Debug.Log("Press O to interact with the character");
            }
        }
 
        public void OnTriggerExit(Collider npc)
        {
            if (npc.CompareTag("Player"))
            {
                triggerActive = false;
                NPCIcon.SetActive(false);
            }
        }
 
        private void Update()
        {
            //Keyboard Action
            if (triggerActive && Input.GetKeyDown(KeyCode.O))
            {
                Talk();
            }

            if (triggerActive && Input.GetKeyDown(KeyCode.Return)) {
                Conversation();
                return;
            }
        }
 
        public void Talk()
        {
            //For controller input
            if (triggerActive) {
                // FindObjectOfType<DialogueTrigger>().TriggerDialogue();

                //Finds the local DialogueTrigger rather than global
                GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }

        public void Conversation() {
            if (triggerActive) { 
                //There is only one DialogueManager so that's why we still use FindObjectOfType
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
}
