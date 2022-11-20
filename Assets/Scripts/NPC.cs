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
    public GameObject NPCActionIcon;
    public GameObject player;
    private DialogueTrigger trigger;
    private bool playerStartConvo;
    private bool completedGoal = false;

    private bool triggerCompletedState = false;
    [SerializeField] private bool levelStateNPC = false;
    //private bool convoActive = false;
    // GameManager gameManager;

    PlayerControls controls;

    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Conversation.performed += ctx => Talk();
        controls.Gameplay.Interact.performed += ctx => Action();
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

            //here about showing the other icon
            if(levelStateNPC == true) {

                //For testing this is 0, should be 1 for Level 1
                if(FindObjectOfType<GameManager>().GetActiveLevel() == 1) {
                    if(player.GetComponent<Player>().playerSelectedItem() == player.GetComponent<Player>().PlayerSelectedApple()) {
                        //show other icon
                        NPCActionIcon.SetActive(true);
                        triggerCompletedState = true;
                    }

                    //Just wanting to talk to the player
                    else {
                        NPCIcon.SetActive(true);
                    }
                }

                //For testing this is 0, should be 2 for Level 2
                if(FindObjectOfType<GameManager>().GetActiveLevel() == 2) {
                    // if(player.GetComponent<Player>().playerSelectedItem() == player.GetComponent<Player>().PlayerSelectedApple()) {
                        //show other icon
                        NPCActionIcon.SetActive(true);
                        triggerCompletedState = true;
                    // }

                        //Just wanting to talk to the player
                    // else {
                    //     NPCIcon.SetActive(true);
                    // }
                }
            }

            else {
                NPCIcon.SetActive(true);
            }

            FindObjectOfType<Player>().WithinCollider(true);
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
            triggerCompletedState = false;

            if(levelStateNPC == true) {
                NPCActionIcon.SetActive(false);
            }

            FindObjectOfType<Player>().WithinCollider(false);
            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().HideControls();
            }
        }
    }

    private void Update()
    {
        playerStartConvo = player.GetComponent<Player>().startConvoActive(); // The player has started a conversation with an NPC

        //Keyboard Action
        if (triggerActive && Input.GetKeyDown(KeyCode.K))
        {
            Talk();
        }

        if (triggerActive && Input.GetKeyDown(KeyCode.J) && triggerCompletedState == true) {
            Action();
        }

        Debug.Log("Trigger Completed State: "+triggerCompletedState);
    }

    public void Action() {
        if(triggerCompletedState == true && completedGoal == false) {
            FindObjectOfType<AudioManager>().Play("Interact"); //Put action sound here

            //Level 1
            if(FindObjectOfType<GameManager>().GetActiveLevel() == 1) {
                player.GetComponent<Player>().UseSelectedItem();
                FindObjectOfType<LevelManager>().AddCompletedTasks();
            }

            //Level 2
            else if(FindObjectOfType<GameManager>().GetActiveLevel() == 0) {
                player.GetComponent<Player>().PickupItem(4);
                NPCObject.SetActive(false);
                FindObjectOfType<Level2Ducks>().AddDuck();
            }

            //something here to make the npc look happier for animation
            //add here to the level count
            completedGoal = true; //This NPC has their item
            // FindObjectOfType<LevelManager>().AddCompletedTasks();
        }
    }

    public void Talk()
    {
        //For controller input
        if (triggerActive) {
            // If the player has not started talking to an NPC
            if(playerStartConvo == true) {

                if(completedGoal == true) {
                    FindObjectOfType<DialogueManager>().CompletedGoal(true);
                }

                else if (completedGoal == false) {
                   FindObjectOfType<DialogueManager>().CompletedGoal(false); 
                }

                FindObjectOfType<AudioManager>().Play("Interact");
                playerStartConvo = false;
                player.GetComponent<Player>().startConvoValue(0);
                GetComponent<DialogueTrigger>().TriggerDialogue();
                return;
            }

            // If the player has already started talking to an NPC
            else if(playerStartConvo == false) {
                FindObjectOfType<AudioManager>().Play("Conversation");
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
                return;
            }
        }
    }

    // public void TriggerCompletedState(bool state) {
    //     triggerCompletedState = state;
    // }

    // public bool ShowtriggerCompletedState() {
    //     return triggerCompletedState;
    // }
}
