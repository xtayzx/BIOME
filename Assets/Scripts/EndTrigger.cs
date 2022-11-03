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

    public void OnTriggerEnter(Collider exit) {
        if (exit.CompareTag("Player"))
            {
              gameManager.CompleteLevel();
            }
    }

    public void LevelSelection() {
        // gameManager.UnfreezeGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }

    public void MainMenu() {
        // gameManager.UnfreezeGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
