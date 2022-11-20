using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Level2Ducks : MonoBehaviour
{
    private int ducksCollected = 0;
    private bool completedTask = false;
    // private bool triggerActive = false;
    // PlayerControls controls;

    // void Awake() {
    //     controls = new PlayerControls();
    //     // controls.Gameplay.Conversation.performed += ctx => Talk();
    //     controls.Gameplay.Interact.performed += ctx => Action();
    // }

    // void OnEnable() {
    //     controls.Gameplay.Enable();
    // }

    // void OnDisable() {
    //     controls.Gameplay.Disable();
    // }
    
    void Update() {
        if(ducksCollected >= 3) {
            completedTask = true;
        }
    }

    public void OnTriggerEnter(Collider exit) {
        if (exit.CompareTag("Player") && completedTask == true)
            {
              FindObjectOfType<LevelManager>().CompleteLevel();
            }
    }
    
    public void AddDuck() {
        ducksCollected++;
    }

    public bool DucksAllSaved() {
        return completedTask;
    }
}
