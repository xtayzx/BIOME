using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEditor;

public class EndTrigger : MonoBehaviour
{
    public GameManager gameManager;
    
     PlayerControls controls;
    private GameObject currentObject;
    public GameObject returnGameSelectionButton;

    void Awake() {
        controls = new PlayerControls();

        controls.Menu.Select.performed += ctx => Select();
        // gameManager.ActivateControls("MenuControls");
         //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(returnGameSelectionButton);
        // gameManager.SelectFirstButton(returnGameSelectionButton);
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

    public void OnTriggerEnter() {
        gameManager.CompleteLevel();
    }

    public void LevelSelection() {
        gameManager.UnfreezeGame();
        SceneManager.LoadScene("LevelSelection");
    }

    public void MainMenu() {
        gameManager.UnfreezeGame();
        SceneManager.LoadScene("Menu");
    }
}
