using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// TODO: able to toggle through the menu with game controller, however still triggers as like gameplay (jump toggled) so don't know need to fix that
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI, resumeButton, menuButton, exitButton;

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
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);

        //freezes the game
        Time.timeScale = 0f;

        GameIsPaused = true;

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(resumeButton);

    }

    public void LoadMenu() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame () {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
