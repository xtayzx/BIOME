using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Level3Ducks : MonoBehaviour
{
    private int ducksCollected = 0;
    private bool completedTask = false;
    
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
