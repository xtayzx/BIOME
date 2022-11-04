using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

#if UNITY_EDITOR 
    using UnityEditor;
#endif

public class EndTrigger : MonoBehaviour
{
    public LevelManager levelManager;
    
    PlayerControls controls;

    // For determining buttons
    private GameObject currentObject;
    public GameObject returnGameSelectionButton;

    void Awake() {
        controls = new PlayerControls();
        controls.Menu.Select.performed += ctx => Select();

        // Clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(returnGameSelectionButton);
    }

    public void Select() {
        #if UNITY_EDITOR
        currentObject = Selection.activeGameObject;
        #endif

        // Debug.Log(currentObject);
    }

    void OnEnable() {
        controls.Menu.Enable();
    }

    void OnDisable() {
        controls.Menu.Disable();
    }

    // If the player enters the finish area, then end the level
    public void OnTriggerEnter(Collider exit) {
        if (exit.CompareTag("Player"))
            {
              levelManager.CompleteLevel();
            }
    }

    // Return to the level hub
    public void LevelSelection() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }

    // Return back to the main menu
    public void MainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
