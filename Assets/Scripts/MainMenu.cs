using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

#if UNITY_EDITOR 
    using UnityEditor;
#endif
// using UnityEditor;

public class MainMenu : MonoBehaviour
{

    // public GameObject optionsMenu, mainMenu;
    public GameObject mainMenuFirstButton, optionsFirstButton, optionsClosedButton;
    
    PlayerControls controls;
    GameObject currentObject;
    // public GameManager gameManager;

    void Awake() {
        controls = new PlayerControls();
         //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        // gameManager.SelectFirstButton(mainMenuFirstButton);

        controls.Menu.Select.performed += ctx => Select();
    }

    public void Select() {
        #if UNITY_EDITOR
        currentObject = Selection.activeGameObject;
        #endif
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
        FindObjectOfType<GameManager>().CurrentActiveLevel(0);
        SceneManager.LoadScene("LevelSelection");
    }

    public void OpenOptions() {
        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        // gameManager.SelectFirstButton(optionsFirstButton);
    }

    public void CloseOptions() {
        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
        // gameManager.SelectFirstButton(optionsClosedButton);
    }

    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
