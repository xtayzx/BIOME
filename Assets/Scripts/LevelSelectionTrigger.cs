using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionTrigger : MonoBehaviour
{
    [SerializeField] private int levelNum;

    // If the player enters the finish area, then end the level
    public void OnTriggerEnter(Collider level) {
        if (level.CompareTag("Player"))
            {
            RunLevel(levelNum);
            }
    }
    public void RunLevel(int num) {
        if (num == 1) {
            SceneManager.LoadScene("Level1");
            FindObjectOfType<GameManager>().CurrentActiveLevel(1);
            return;
        }

        else if (num == 2) {
            SceneManager.LoadScene("Level2");
        FindObjectOfType<GameManager>().CurrentActiveLevel(2);
        return;
        }

        // else if (num == 3) {
        //     SceneManager.LoadScene("Menu");
        //     FindObjectOfType<GameManager>().CurrentActiveLevel(3);
        //     return;
        // }
    }
}
