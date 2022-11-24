using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI, resumeButton, menuButton, exitButton, inventory, checkpoint, inventoryControls;
    PlayerControls controls;

    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Menu.performed += ctx => Pause();
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
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        FindObjectOfType<AudioManager>().Play("MainSong");
        pauseMenuUI.SetActive(false);

        inventory.SetActive(true);
        checkpoint.SetActive(true);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            inventoryControls.SetActive(true);
        }

        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<GameManager>().SetPaused(false);
    }

    void Pause() {
        if (GameIsPaused) {
            Resume();
            return;
        }
        
        FindObjectOfType<AudioManager>().Pause("MainSong");
        pauseMenuUI.SetActive(true);
        inventory.SetActive(false);
        checkpoint.SetActive(false);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            inventoryControls.SetActive(false);
        }
        
        //Freezes the game
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<GameManager>().SetPaused(true);

        //Clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //Set a new selected object
        EventSystem.current.SetSelectedGameObject(resumeButton);

    }

    public void LoadMenu() {

        // Hide and load elements
        pauseMenuUI.SetActive(false);
        inventory.SetActive(true);
        checkpoint.SetActive(true);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            inventoryControls.SetActive(true);
        }

        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void ResetGame() {

         // Hide and load elements
        pauseMenuUI.SetActive(false);
        inventory.SetActive(true);
        checkpoint.SetActive(true);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            inventoryControls.SetActive(true);
        }

        Time.timeScale = 1f;
        GameIsPaused = false;

        // Reset all objects in the level
        PushObject[] pushObjects = FindObjectsOfType<PushObject>();
        for (int i = 0; i < pushObjects.Length; i++) {
            {
                pushObjects[i].ObjectStartPosition();
            }
        }

        FindObjectOfType<Player>().StartAtCheckpoint();
        FindObjectOfType<AudioManager>().Play("MainSong");

        //TODO: what does this classify, what is the reset?
    }

    public void FallResetGame() {
        FindObjectOfType<Player>().StartAtCheckpoint();
        FindObjectOfType<AudioManager>().Play("Checkpoint");

        //TODO: what does this classify, what is the reset?
    }

    public void QuitGame () {
        // Debug.Log("QUIT!");
        Application.Quit();
    }
}
