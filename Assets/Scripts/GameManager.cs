using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    PlayerControls controls;
    public int activeLevel = 0;
    private int completedLevel = 0;
    private int totalScore = 0;

    public int[] levelScores = new int[3];
    private int levels = 3; //There are 3 levels in the game

    public static GameManager instance;
    private bool gamePaused = false;

    void Awake() {
        //So only one instance of GameManager is created and is carried through each scene
        DontDestroyOnLoad(this);
        if (instance == null) {
            instance = this;
        }

        else {
            Destroy(gameObject);
            return;
        }
    }

    void Start () {
        //Set up game configurations, frame rate and screen ratio
        Application.targetFrameRate = 60;
        // Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen, Screen.currentResolution.refreshRate);
        Screen.SetResolution(1280, 720, false);
        // Declare array that determines how many level scores
        for(int i = 0; i < levels; i++) {
            levelScores[i] = 0;
        }
    }

    public void LevelScore(int score) {
        if(activeLevel == 1) {
            if(score >= levelScores[0]){
                levelScores[0] = score;
            }
        }

        else if(activeLevel == 2) {
            if(score >= levelScores[1]){
                levelScores[1] = score;
            }
        }

        else if(activeLevel == 3) {
            if(score >= levelScores[2]){
                levelScores[2] = score;
            }
        }
    }

    // Determine the current level being played
    public void CurrentActiveLevel(int num) {
        activeLevel = num;
    }

    // Return current level played
    public int GetActiveLevel() {
        return activeLevel;
    }

    public void AddCompletedLevel() {
        completedLevel++;
    }

    public int CompletedLevelValue() {
        return completedLevel;
    }

    public bool IsGamePaused() {
        return gamePaused;
    }

    public void SetPaused(bool paused) {
        gamePaused = paused;
    }

    // Return total score across levels
    public int TotalScoreValue() {
        for(int i = 0; i < levels; i++) {
            totalScore += levelScores[i];
        }
        return totalScore;
    }

    public int LevelScoreValue(int num) {
        if(num == 0) {
            return levelScores[0];
        }

        else if (num == 1) {
            return levelScores[1];
        }

        else if (num == 2) {
            return levelScores[2];
        }

        else return totalScore;
    }

    // STUFF FOR TRYING TO INTEGRATE SAVE COMPONENT BUT CURRENTLY NOT WORKING
    // public void SaveGameManager() {
    //     SaveGame.SaveGameManager(this);
    // }

    // public void LoadGameManager() {
    //     GameData data = SaveGame.LoadGameData();

    //     activeLevel = data.activeLevel;

    //     for (int i = 0; i < 3; i++) {
    //         levelScores[i] = data.levelScores[i];
    //     }
    // }
}
