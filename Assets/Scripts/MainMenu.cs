using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// TODO: game controller works with navigation, however cannot select - need to fix going in and out of options menu as well

public class MainMenu : MonoBehaviour
{

    // TODO: currently below does nothing
    // public GameObject mainMenuFirstButton, optionsFirstButton, optionsClosedButton;

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("QUIT!");
        // Application.Quit();
    }
}
