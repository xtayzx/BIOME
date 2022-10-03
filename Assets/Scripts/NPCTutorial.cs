using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NPCTutorial : MonoBehaviour
{

    [SerializeField] private bool triggerActive = false;

    public GameObject NPCIcon;
    private DialogueTrigger trigger;
    // private bool nextSentence = false;

    PlayerControls controls;

        void Awake() {
            controls = new PlayerControls();

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
                FindObjectOfType<DialogueTrigger>().TriggerDialogue();
                // Debug.Log("HELLO!");
            }
        }

        public void Conversation() {
            if (triggerActive) { 
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
}
