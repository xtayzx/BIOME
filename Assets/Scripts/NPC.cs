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
            triggerActive = true;

            //here about showing the other icon
            if(levelStateNPC == true) {

                //For testing this is 0, should be 1 for Level 1
                if(FindObjectOfType<GameManager>().GetActiveLevel() == 1) {
                    if(player.GetComponent<Player>().playerSelectedItem() == player.GetComponent<Player>().PlayerSelectedApple()) {
                        //show other icon
                        NPCActionIcon.SetActive(true);
                        triggerCompletedState = true;
                        FindObjectOfType<AudioManager>().Play("Action");
                    }

                    //Just wanting to talk to the player
                    else {
                        NPCIcon.SetActive(true);
                        FindObjectOfType<AudioManager>().Play("Object");
                    }
                }

                else if(FindObjectOfType<GameManager>().GetActiveLevel() == 2) {
                    if(player.GetComponent<Player>().playerSelectedItem() == player.GetComponent<Player>().PlayerSelectedApple()) {
                        //show other icon
                        NPCActionIcon.SetActive(true);
                        triggerCompletedState = true;
                        FindObjectOfType<AudioManager>().Play("Action");
                    }

                    //Just wanting to talk to the player
                    else {
                        NPCIcon.SetActive(true);
                        FindObjectOfType<AudioManager>().Play("Object");
                    }
                }

                else if(FindObjectOfType<GameManager>().GetActiveLevel() == 3) {
                        NPCActionIcon.SetActive(true);
                        triggerCompletedState = true;
                        FindObjectOfType<AudioManager>().Play("Action");
                }
            }

            else {
                NPCIcon.SetActive(true);
                FindObjectOfType<AudioManager>().Play("Object");
            }

            FindObjectOfType<Player>().WithinCollider(true);
            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().ShowTalk();
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
                FindObjectOfType<Tutorial>().HideTalk();
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
    }

    public void Action() {
        if(triggerCompletedState == true && completedGoal == false) {

            //Level 1
            if(FindObjectOfType<GameManager>().GetActiveLevel() == 1) {
                player.GetComponent<Player>().UseSelectedItem();
                FindObjectOfType<LevelManager>().AddCompletedTasks();
                FindObjectOfType<AudioManager>().Play("AppleCrunch");
            }

            //Level 2
            else if(FindObjectOfType<GameManager>().GetActiveLevel() == 2) {
                player.GetComponent<Player>().UseSelectedItem();
                FindObjectOfType<LevelManager>().FinishLevel2();
                FindObjectOfType<AudioManager>().Play("AppleCrunch");
            }

            //Level 3
            else if(FindObjectOfType<GameManager>().GetActiveLevel() == 3) {
                player.GetComponent<Player>().PickupItem(4);
                NPCObject.SetActive(false);
                FindObjectOfType<Level3Ducks>().AddDuck();
                FindObjectOfType<AudioManager>().Play("DuckQuack");
            }
            completedGoal = true; //This NPC has their item
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
                player.GetComponent<Player>().IdleAnimation(); //Change back to idle animation
                FindObjectOfType<PauseMenu>().TalkingStatus(true); //Player cannot pause game (closing method in DialogueManager)
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

    public bool ShowCompleteGoal() {
        return completedGoal;
    }

    public void ChangeCompletedGoal(bool state) {
        completedGoal = state;
    }

    public void TriggerActiveState(bool state) {
        triggerActive = state;
    }

    public void TriggerCompletedState(bool state) {
        triggerCompletedState = state;
    }

    public void ResetActionIcon() {
        NPCActionIcon.SetActive(false);
    }
}
