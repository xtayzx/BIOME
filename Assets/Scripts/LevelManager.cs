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
    PlayerControls controls;
    public GameObject player;
    [SerializeField] private bool tutorial;
    private int levelScore;

    public void CompleteLevel() {

        FreezeGame();
        FindObjectOfType<AudioManager>().Pause("MainSong");
        FindObjectOfType<AudioManager>().Play("End");
        FindObjectOfType<LevelItems>().ShowItemsEnd();
        FindObjectOfType<Timer>().StopTimer();
        levelScore = FindObjectOfType<Timer>().FinalLevelScore();

        FindObjectOfType<GameManager>().LevelScore(levelScore);

        // GameIsPaused = true;
        Debug.Log("LEVEL WON");

        //Hide the inventory and show the items collected/statistics
        controlsUI.SetActive(false);
        completeLevelUI.SetActive(true);
    }

    public bool Tutorial() {
        return tutorial;
    }

    public void FreezeGame() {
        //freezes the game
        Time.timeScale = 0f;
    }

    //When the End Level menu pops up, the next level button is selected first
    public void SelectFirstButton(GameObject firstButton) {

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(firstButton);

    }
}
