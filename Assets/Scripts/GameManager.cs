using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    // public GameObject completeLevelUI;
    // public GameObject controlsUI;
    // public GameObject audioManager;
    // public GameObject gameManager;
    PlayerControls controls;
    // public GameObject player;
    // [SerializeField] private bool tutorial;
    private int activeLevel;
    private int totalScore = 0;
    // private int levelOneScore = 0;
    // private int levelTwoScore = 0;
    // private int levelThreeScore = 0;
    private int[] levelScores = new int[3];
    private int levels = 3; //There are 3 levels in the game

    void Start () {
        // Set game frame rate - cause my fans are going crazy so I think this sets it up
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen, Screen.currentResolution.refreshRate);
        // trigger = false;

        for(int i = 0; i < 3; i++) {
            levelScores[i] = 0;
        }
    }

    public void LevelScore(int score) {

        //Need to add here that if the player gets a better score than previously played, than that score is replaced when calculating the main score
        if(activeLevel == 1) {
            levelScores[0] = score;
            // totalScore = totalScore + levelOneScore;
        }

        else if(activeLevel == 2) {
            levelScores[1] = score;
            // totalScore = totalScore + levelTwoScore;
        }

        else if(activeLevel == 3) {
            levelScores[2] = score;
            // totalScore = totalScore + levelThreeScore;
        }
    }

    public void CurrentActiveLevel(int num) {
        activeLevel = num;
    }

    public int GetActiveLevel() {
        return activeLevel;
    }

    public int TotalScoreValue() {
        for(int i = 0; i < levels; i++) {
            totalScore += levelScores[i];
        }
        return totalScore;
    }

    // public void CompleteLevel() {

    //     FreezeGame();
    //     FindObjectOfType<AudioManager>().Pause("MainSong");
    //     FindObjectOfType<AudioManager>().Play("End");
    //     FindObjectOfType<LevelItems>().ShowItemsEnd();

    //     // GameIsPaused = true;
    //     Debug.Log("LEVEL WON");

    //     //Hide the inventory and show the items collected/statistics
    //     controlsUI.SetActive(false);
    //     completeLevelUI.SetActive(true);
    // }

    // public bool Tutorial() {
    //     return tutorial;
    // }

    // public void FreezeGame() {
    //     //freezes the game
    //     Time.timeScale = 0f;
    // }

    // //When the End Level menu pops up, the next level button is selected first
    // public void SelectFirstButton(GameObject firstButton) {

    //     //clear selected object 
    //     EventSystem.current.SetSelectedGameObject(null);
    //     //set a new selected object
    //     EventSystem.current.SetSelectedGameObject(firstButton);

    // }
}
