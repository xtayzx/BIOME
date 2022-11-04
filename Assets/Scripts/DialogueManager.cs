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

        // Debug.Log("Starting conversation with "+ dialogue.name);
    
        nameText.text = dialogue.name;

        // Clear the queued sentences and then load the sentences for the NPC being interacted with
        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        //Load the image of the NPC speaking
        image.sprite = dialogue.sprite;

        //Start the conversation
        DisplayNextSentence();
    }

    public void DisplayNextSentence () {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        //Get next in queue
        string sentence = sentences.Dequeue();

        //Wait till last sentence finishes
        StopAllCoroutines();

        //Animate next sentence
        StartCoroutine(TypeSentence(sentence));
        
        // dialogueText.text = sentence;
        // Debug.Log(sentence);
    }

    // Animate the sentence
    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
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
    }

}
