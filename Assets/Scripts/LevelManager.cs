using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    public GameObject completeLevelUI;
    public GameObject controlsUI;
    public GameObject player;

    [SerializeField] private bool tutorial;
    PlayerControls controls;
    private int levelScore;

    // When the player reaches the finish of the level 
    public void CompleteLevel() {
        FreezeGame();

        // Play sounds
        FindObjectOfType<AudioManager>().Pause("MainSong");
        FindObjectOfType<AudioManager>().Play("End");

        FindObjectOfType<LevelItems>().ShowItemsEnd(); //Show items collected in the level
        FindObjectOfType<Timer>().StopTimer();
        levelScore = FindObjectOfType<Timer>().FinalLevelScore(); //Set final level score

        FindObjectOfType<GameManager>().LevelScore(levelScore); //Return back to Game Manager

        // Debug.Log("LEVEL WON");

        //Hide the inventory and show the items collected/statistics
        controlsUI.SetActive(false);
        completeLevelUI.SetActive(true);
    }

    // Is this level 1 with the UI controls
    public bool Tutorial() {
        return tutorial;
    }

    public void FreezeGame() {
        //Freezes the game
        Time.timeScale = 0f;
    }

    //When the End Level menu pops up, the next level button is selected first
    public void SelectFirstButton(GameObject firstButton) {

        //Clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //Set a new selected object
        EventSystem.current.SetSelectedGameObject(firstButton);

    }
}
