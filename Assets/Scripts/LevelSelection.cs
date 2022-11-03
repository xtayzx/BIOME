using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

#if UNITY_EDITOR 
    using UnityEditor;
#endif
// using UnityEditor;

public class LevelSelection : MonoBehaviour
{
    // public GameObject optionsMenu, mainMenu;
    public GameObject levelFirstButton, levelOneButton, backButton;
    
    PlayerControls controls;
    GameObject currentObject;
    private int totalScoreValue;
    public TextMeshProUGUI totalScore;
    // public GameManager gameManager;

    void Awake() {
        controls = new PlayerControls();
         //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(levelFirstButton);

        // gameManager.SelectFirstButton(levelFirstButton);

        controls.Menu.Select.performed += ctx => Select();
        totalScoreValue = FindObjectOfType<GameManager>().TotalScoreValue();
        totalScore.text = totalScoreValue.ToString();
    }

    void OnEnable() {
        controls.Menu.Enable();
    }

    void OnDisable() {
        controls.Menu.Disable();
    }

    public void Select() {
        #if UNITY_EDITOR
        currentObject = Selection.activeGameObject;
        #endif
        // buttonPressed = true;
        Debug.Log("Button Pressed");

        // if(currentObject = tutorialLevel) {
        //     Tutorial();
        //     return;
        // }

        // else if(currentObject = levelOneButton) {
        //     LevelOne();
        //     return;
        // }

        // else if(currentObject = backButton) {
        //     MainMenu();
        //     return;
        // }
    } 

    public void LevelOne() {
        SceneManager.LoadScene("Level1");
        FindObjectOfType<GameManager>().CurrentActiveLevel(1);
    }

    public void LevelTwo() {
        // Debug.Log("Currently not avaliable to play");
        SceneManager.LoadScene("Level2");
        FindObjectOfType<GameManager>().CurrentActiveLevel(2);
    }

    public void MainMenu() {
        SceneManager.LoadScene("Menu");
        FindObjectOfType<GameManager>().CurrentActiveLevel(3);
    }
}

