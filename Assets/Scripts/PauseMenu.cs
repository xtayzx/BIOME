using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI, resumeButton, menuButton, exitButton;

    public GameManager gameManager;

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
        pauseMenuUI.SetActive(false);
        gameManager.UnfreezeGame();
        GameIsPaused = false;
    }

    void Pause() {
        if (GameIsPaused) {
            Resume();
            return;
        }
        
        pauseMenuUI.SetActive(true);

        //freezes the game
        gameManager.FreezeGame();

        GameIsPaused = true;

        // gameManager.SelectFirstButton(resumeButton);

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(resumeButton);

    }

    public void LoadMenu() {
        pauseMenuUI.SetActive(false);
        gameManager.UnfreezeGame();
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void ResetGame() {
        pauseMenuUI.SetActive(false);
        gameManager.UnfreezeGame();
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //TODO: what does this classify, what is the reset?
    }

    public void QuitGame () {
        Debug.Log("QUIT!");
        // Application.Quit();
    }
}
