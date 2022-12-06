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

public class LevelSelection : MonoBehaviour
{
    PlayerControls controls;
    GameObject currentObject;
    private int totalScoreValue;
    public TextMeshProUGUI totalScore;

    void Awake() {
        
         //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);

        //Enable controller functionality
        controls = new PlayerControls();
        controls.Menu.Select.performed += ctx => Select();

        //Total Score Text
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

        //Determine what button is selected
        #if UNITY_EDITOR
        currentObject = Selection.activeGameObject;
        #endif

    } 

    // LOAD LEVELS

    public void LevelOne() {
        SceneManager.LoadScene("Level1");
        FindObjectOfType<GameManager>().CurrentActiveLevel(1);
    }

    public void LevelTwo() {
        SceneManager.LoadScene("Level2");
        FindObjectOfType<GameManager>().CurrentActiveLevel(2);
    }

    public void MainMenu() {
        SceneManager.LoadScene("Menu");
        FindObjectOfType<GameManager>().CurrentActiveLevel(3);
    }
}

