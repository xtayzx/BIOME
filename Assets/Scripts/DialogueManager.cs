using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image image;
    public GameObject player;
    private Queue<string> sentences;
    private bool skip = false;
    private string currentSentence;

    private bool showSecondarySentences = false;

    public Animator animator;
    public Animator animatorInventory;
    public Animator checkpoint;
   
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue) {

        // Moving around UI elements to be opened
        animator.SetBool("IsOpen", true);
        animatorInventory.SetBool("IsMoved", true);
        checkpoint.SetBool("OpenConvo", true);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            FindObjectOfType<Tutorial3>().MoveInventory(true);
        }
    
        nameText.text = dialogue.name;

        //Load the image of the NPC speaking
        image.sprite = dialogue.sprite;

        if(showSecondarySentences == false) {
            // Clear the queued sentences and then load the sentences for the NPC being interacted with
            sentences.Clear();
            foreach (string sentence in dialogue.sentences) {
                sentences.Enqueue(sentence);
            }

            //Start the conversation
            DisplayNextSentence();
        }

        else if(showSecondarySentences == true) {
            // Clear the queued sentences and then load the sentences for the NPC being interacted with
            sentences.Clear();
            foreach (string sentence in dialogue.otherSentences) {
                sentences.Enqueue(sentence);
            }

            //Start the conversation
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence () {

        //If player would like to skip the loading of the dialogue
        if(skip == true) {
            StopAllCoroutines();
            dialogueText.text = "";
            dialogueText.text = currentSentence;
            skip = false;
        }
        
        //Loading dialogue from start and typing animation
        else if(skip == false) {
            if (sentences.Count == 0) {
            EndDialogue();
            return;
            }

            //Get next in queue
            string sentence = sentences.Dequeue();
            currentSentence = sentence;

            //Wait till last sentence finishes
            StopAllCoroutines();

            //Animate next sentence
            StartCoroutine(TypeSentence(sentence));
            skip = true;
        }
    }

    // Animate the sentence
    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        skip = false;
    }

    // When the dialogue is finished
    void EndDialogue() {
        animator.SetBool("IsOpen", false);
        animatorInventory.SetBool("IsMoved", false);
        checkpoint.SetBool("OpenConvo", false);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            FindObjectOfType<Tutorial3>().MoveInventory(false);
        }

        // Say the player can now talk interact with a new NPC
        player.GetComponent<Player>().startConvoValue(1);
        FindObjectOfType<PauseMenu>().TalkingStatus(false); //Player can now pause game
    }

    public void CompletedGoal(bool state) {
        showSecondarySentences = state;
    }

    public bool ShowStateCompletedGoal() {
        return showSecondarySentences;
    }

}
