using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float currentTime;
    int currentTimeScore;

    bool timerActive = true;
    bool subtractScore = true;

    public int startMinutes;
    private int calculatedLevelScore;

    public TextMeshProUGUI currentTimeText;

    void Start()
    {
        currentTime = startMinutes * 60;
        calculatedLevelScore = (int)currentTime; //Type case from float to int
    }

    // Update is called once per frame
    void Update()
    {
        //For every 5 seconds, subtract one from the main score of the level. The score is set in Unity from the Level Manager
        if (timerActive == true) {
            currentTime = currentTime - Time.deltaTime;
            currentTimeScore = (int)currentTime;

            //If the modulus is divisible by 5 (or is a multiple of 5), then change the score
            if(((int)currentTimeScore % 5 == 0) && subtractScore == true) {
                calculatedLevelScore--;
                subtractScore = false; //The boolean makes sure that this does not read the float value, otherwise it subtracts 60
                return;
            }

            else if ((int)currentTimeScore % 5 != 0) {
                subtractScore = true;
            }
        }
        currentTimeText.text = calculatedLevelScore.ToString();
    }

    public void StartTimer() {
        timerActive = true;
    }

    public void StopTimer() {
        timerActive = false;
    }

    public int FinalLevelScore(){
        return calculatedLevelScore;
    }

    public int Score() {
        return calculatedLevelScore;
    }
}