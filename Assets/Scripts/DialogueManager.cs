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
   
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue) {

        animator.SetBool("IsOpen", true);
        animatorInventory.SetBool("IsMoved", true);
        checkpoint.SetBool("OpenConvo", true);

        if(FindObjectOfType<GameManager>().Tutorial() == true) {
            FindObjectOfType<Tutorial3>().MoveInventory(true);
        }


        Debug.Log("Starting conversation with "+ dialogue.name);
        
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        image.sprite = dialogue.sprite;

        DisplayNextSentence();
    }

    public void DisplayNextSentence () {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        //get next in queue
        string sentence = sentences.Dequeue();

        //wait till last sentence finishes
        StopAllCoroutines();

        //animate next sentence
        StartCoroutine(TypeSentence(sentence));
        
        // dialogueText.text = sentence;
        // Debug.Log(sentence);
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue() {
        animator.SetBool("IsOpen", false);
        animatorInventory.SetBool("IsMoved", false);
        checkpoint.SetBool("OpenConvo", false);

        if(FindObjectOfType<GameManager>().Tutorial() == true) {
            FindObjectOfType<Tutorial3>().MoveInventory(false);
        }

        player.GetComponent<Player>().startConvoValue(1);
        // Debug.Log("End of conversation");
    }

}
