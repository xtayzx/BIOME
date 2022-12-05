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
    private int calculatedLevelScore; //Actual score value
    private int calculatedTimerValue; //Timer value

    public TextMeshProUGUI currentTimeText;

    void Start()
    {
        currentTime = startMinutes * 60;
        calculatedLevelScore = (int)currentTime; //Type case from float to int
        calculatedTimerValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //For every 5 seconds, subtract one from the main score of the level. The score is set in Unity from the Level Manager
        if (timerActive == true) {
            currentTime = currentTime - Time.deltaTime;
            currentTimeScore = (int)currentTime;

            //If the modulus is divisible by 2 (or is a multiple of 2), then change the score
            if(((int)currentTimeScore % 2 == 0) && subtractScore == true) {
                calculatedLevelScore--;
                calculatedTimerValue++;
                subtractScore = false; //The boolean makes sure that this does not read the float value, otherwise it subtracts 60
                return;
            }

            else if ((int)currentTimeScore % 2 != 0) {
                subtractScore = true;
            }
        }
        currentTimeText.text = calculatedTimerValue.ToString();
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
