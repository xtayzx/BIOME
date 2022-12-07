using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelHubChangeDialogue : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject thisObject;

    private int completedLevel;
    [SerializeField] private bool Level1Done;
    [SerializeField] private bool Level2Done;
    [SerializeField] private bool Level3Done;

    void Awake() {
        completedLevel = FindObjectOfType<GameManager>().CompletedLevelValue();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(completedLevel >= 1 && Level1Done == true) {
            GetComponent<NPC>().ChangeCompletedGoal(true);
        }

        else if(completedLevel >= 2 && Level2Done == true) {
            GetComponent<NPC>().ChangeCompletedGoal(true);
        }

        else if(completedLevel >= 3 && Level3Done == true) {
            GetComponent<NPC>().ChangeCompletedGoal(true);
        }
    }
}
