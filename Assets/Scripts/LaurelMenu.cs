using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LaurelMenu : MonoBehaviour
{
    public static bool LaurelLoaded = false;
    public GameObject laurelMenuUI, inventoryControls, inventory, checkpoint;
    PlayerControls controls;
    private GameManager gameManager;

    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Objective.performed += ctx => Objective();
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    void Update()
    {
        //ESC pauses the game
        if(Input.GetKeyDown(KeyCode.I)) {
            if (LaurelLoaded) {
                Resume();
            }
            else {
                Objective();
            }
        }
    }

    public void Resume() {
        // FindObjectOfType<AudioManager>().Play("MainSong");
        laurelMenuUI.SetActive(false);
        inventory.SetActive(true);
        checkpoint.SetActive(true);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            inventoryControls.SetActive(true);
        }

        Time.timeScale = 1f;
        LaurelLoaded = false;
        FindObjectOfType<GameManager>().SetPaused(false);
    }

    public void Objective() {
        if (LaurelLoaded) {
            Resume();
            return;
        }
        
        // FindObjectOfType<AudioManager>().Pause("MainSong");
        laurelMenuUI.SetActive(true);
        inventory.SetActive(false);
        checkpoint.SetActive(false);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            inventoryControls.SetActive(false);
        }
        
        //Freezes the game
        Time.timeScale = 0f;
        LaurelLoaded = true;
        FindObjectOfType<GameManager>().SetPaused(true);

        // //Clear selected object 
        // EventSystem.current.SetSelectedGameObject(null);
        // //Set a new selected object
        // EventSystem.current.SetSelectedGameObject(resumeButton);

    }

    // public void LoadMenu() {

    //     // Hide and load elements
    //     pauseMenuUI.SetActive(false);
    //     inventory.SetActive(true);
    //     checkpoint.SetActive(true);

    //     if(FindObjectOfType<LevelManager>().Tutorial() == true) {
    //         inventoryControls.SetActive(true);
    //     }

    //     Time.timeScale = 1f;
    //     GameIsPaused = false;
    //     SceneManager.LoadScene("Menu");
    // }

    // public void ResetGame() {

    //      // Hide and load elements
    //     pauseMenuUI.SetActive(false);
    //     inventory.SetActive(true);
    //     checkpoint.SetActive(true);

    //     if(FindObjectOfType<LevelManager>().Tutorial() == true) {
    //         inventoryControls.SetActive(true);
    //     }

    //     Time.timeScale = 1f;
    //     GameIsPaused = false;

    //     // Reset all objects in the level
    //     PushObject[] pushObjects = FindObjectsOfType<PushObject>();
    //     for (int i = 0; i < pushObjects.Length; i++) {
    //         {
    //             pushObjects[i].ObjectStartPosition();
    //         }
    //     }

    //     FindObjectOfType<Player>().StartAtCheckpoint();
    //     FindObjectOfType<AudioManager>().Play("MainSong");

    //     //TODO: what does this classify, what is the reset?
    // }

    // public void FallResetGame() {
    //     FindObjectOfType<Player>().StartAtCheckpoint();
    //     FindObjectOfType<AudioManager>().Play("Checkpoint");

    //     //TODO: what does this classify, what is the reset?
    // }

    // public void QuitGame () {
    //     // Debug.Log("QUIT!");
    //     Application.Quit();
    // }
}