using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public GameObject completeLevelUI;
    PlayerControls controls;
    public GameObject player;
    [SerializeField] private bool tutorial;

    void Start () {
        // Set game frame rate - cause my fans are going crazy so I think this sets it up
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen, Screen.currentResolution.refreshRate);
        // trigger = false;
    }

    public void CompleteLevel() {

        FreezeGame();
        FindObjectOfType<AudioManager>().Pause("MainSong");
        FindObjectOfType<AudioManager>().Play("End");

        // GameIsPaused = true;
        Debug.Log("LEVEL WON");
        completeLevelUI.SetActive(true);
    }

    public bool Tutorial() {
        return tutorial;
    }

    public void FreezeGame() {
        //freezes the game
        Time.timeScale = 0f;
    }

    public void SelectFirstButton(GameObject firstButton) {

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(firstButton);

    }
}
