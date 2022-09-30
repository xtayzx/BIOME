using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEditor;

// TODO: game controller works with navigation, however cannot select - need to fix going in and out of options menu as well
// UPDATE: selects but only selects start game with remote, doesn't activate anything else
// also should change controller direction as still uses "jump" from gameplay functionality
public class MainMenu : MonoBehaviour
{

    // TODO: currently below does nothing - update it doesn't really help
    public GameObject mainMenuFirstButton, optionsButton, quitButton;
    
    PlayerControls controls;
    GameObject currentObject;

    void Awake() {
        controls = new PlayerControls();

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);

        controls.Gameplay.Jump.performed += ctx => Select();
    }

    public void Select() {
        currentObject = Selection.activeGameObject;
        // buttonPressed = true;
        Debug.Log(currentObject);

        if(currentObject = mainMenuFirstButton) {
            PlayGame();
            return;
        }

        else if(currentObject = quitButton) {
            QuitGame();
            return;
        }
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("QUIT!");
        // Application.Quit();
    }
}
