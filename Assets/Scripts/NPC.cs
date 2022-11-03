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
    public GameObject player;
    private DialogueTrigger trigger;
    private bool playerStartConvo;
    //private bool convoActive = false;
    GameManager gameManager;

    PlayerControls controls;

        void Awake() {
            controls = new PlayerControls();

            // gameManager.ActivateControls("PlayControls");
            controls.Gameplay.Interact.performed += ctx => Talk();
            // controls.Gameplay.Conversation.performed += ctx => Conversation();
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
                FindObjectOfType<AudioManager>().Play("Object");
                triggerActive = true;
                NPCIcon.SetActive(true);
                
                if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().ShowControls();
                }
            }
        }
 
        public void OnTriggerExit(Collider npc)
        {
            if (npc.CompareTag("Player"))
            {
                triggerActive = false;
                NPCIcon.SetActive(false);

                if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().HideControls();
                }
            }
        }
 
        private void Update()
        {
            playerStartConvo = player.GetComponent<Player>().startConvoActive();
            //Keyboard Action
            if (triggerActive && Input.GetKeyDown(KeyCode.E))
            {
                Talk();
            }

            // if (triggerActive && Input.GetKeyDown(KeyCode.Return)) {
            //     Conversation();
            //     return;
            // }
        }
 
        //TODO: implement conversation method here bc using the triangle button is not working
        public void Talk()
        {
            //For controller input
            if (triggerActive) {
                if(playerStartConvo == true) {
                    FindObjectOfType<AudioManager>().Play("Interact");
                    playerStartConvo = false;
                    player.GetComponent<Player>().startConvoValue(0);
                    GetComponent<DialogueTrigger>().TriggerDialogue();
                    return;
                }

                else if(playerStartConvo == false) {
                    FindObjectOfType<AudioManager>().Play("Conversation");
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();
                    return;
                }
                // FindObjectOfType<DialogueTrigger>().TriggerDialogue();

                //Finds the local DialogueTrigger rather than global
                
            }
        }

        // public void Conversation() {
        //     if (triggerActive) { 
        //         //There is only one DialogueManager so that's why we still use FindObjectOfType
                
        // }
    //}
}
