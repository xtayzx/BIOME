using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EndGame : MonoBehaviour
{
    private int completedLevel;

    void Awake() {
        completedLevel = FindObjectOfType<GameManager>().CompletedLevelValue();
    }

    // If the player enters the finish area, then end the level
    public void OnTriggerEnter(Collider level) {
        if (level.CompareTag("Player"))
            {
                if(completedLevel == 3) {
                    SceneManager.LoadScene("Intro"); //CHANGE
                }
            }
    }
}
