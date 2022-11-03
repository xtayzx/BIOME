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

    // public GameManager gameManager;

    PlayerControls controls;

    void Awake() {
        controls = new PlayerControls();
        // gameManager.ActivateControls("PauseControls");
        controls.Gameplay.Menu.performed += ctx => Pause();
        // gameManager.ActivateControls("PauseControls");
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
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
        // gameManager.UnfreezeGame();
        GameIsPaused = false;
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
        

        //freezes the game
        // gameManager.FreezeGame();
        Time.timeScale = 0f;

        GameIsPaused = true;

        // gameManager.SelectFirstButton(resumeButton);

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(resumeButton);

    }

    public void LoadMenu() {
        pauseMenuUI.SetActive(false);
        // gameManager.UnfreezeGame();

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
        pauseMenuUI.SetActive(false);
        // gameManager.UnfreezeGame();

        inventory.SetActive(true);
        checkpoint.SetActive(true);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            inventoryControls.SetActive(true);
        }

        Time.timeScale = 1f;
        GameIsPaused = false;

        PushObject[] pushObjects = FindObjectsOfType<PushObject>();
        for (int i = 0; i < pushObjects.Length; i++) {
            {
                pushObjects[i].ObjectStartPosition();
            }
        }

        FindObjectOfType<Player>().StartAtCheckpoint();
        // FindObjectOfType<MainCamera>().ResetCamera();
        FindObjectOfType<AudioManager>().Play("MainSong");

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //TODO: what does this classify, what is the reset?
    }

    public void FallResetGame() {
        // pauseMenuUI.SetActive(false);
        // gameManager.UnfreezeGame();
        // Time.timeScale = 1f;
        // GameIsPaused = false;

        // PushObject[] pushObjects = FindObjectsOfType<PushObject>();
        // for (int i = 0; i < pushObjects.Length; i++) {
        //     {
        //         pushObjects[i].ObjectStartPosition();
        //     }
        // }

        // FindObjectOfType<MainCamera>().ResetCamera();
        FindObjectOfType<Player>().StartAtCheckpoint();
        FindObjectOfType<AudioManager>().Play("Checkpoint");

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //TODO: what does this classify, what is the reset?
    }

    public void QuitGame () {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
