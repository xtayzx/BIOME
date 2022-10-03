using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEditor;

public class MainMenu : MonoBehaviour
{

    // public GameObject optionsMenu, mainMenu;
    public GameObject mainMenuFirstButton, optionsFirstButton, optionsClosedButton;
    
    PlayerControls controls;
    GameObject currentObject;

    void Start () {
        // Set game frame rate - cause my fans are going crazy so I think this sets it up
        Application.targetFrameRate = 100;
    }

    void Awake() {
        controls = new PlayerControls();

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);

        controls.Menu.Select.performed += ctx => Select();
    }

    public void Select() {
        currentObject = Selection.activeGameObject;
        // buttonPressed = true;
        Debug.Log(currentObject);

        // if(currentObject = mainMenuFirstButton) {
        //     PlayGame();
        //     return;
        // }

        // else if(currentObject = quitButton) {
        //     QuitGame();
        //     return;
        // }
    }

    void OnEnable() {
        controls.Menu.Enable();
    }

    void OnDisable() {
        controls.Menu.Disable();
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenOptions() {
        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptions() {
        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    public void QuitGame() {
        Debug.Log("QUIT!");
        // Application.Quit();
    }
}
