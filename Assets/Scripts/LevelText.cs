using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelText : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public Animator animator;
    private int selectedLevel;
    private int levelScore = 0;
    private TextMeshProUGUI levelScoreText;
 
     void Update () {
        selectedLevel = FindObjectOfType<Player>().PlayerSelectedLevel();
        // levelScoreText = levelScore.ToString();
     }

    public void ShowText() {
        animator.SetBool("IsSelected", true);
        if (selectedLevel == 1) {
            levelScore = FindObjectOfType<GameManager>().LevelScoreValue(selectedLevel-1);
            levelText.text = "LEVEL 1 SCORE: "+levelScore;
        }

        else if (selectedLevel == 2) {
            levelScore = FindObjectOfType<GameManager>().LevelScoreValue(selectedLevel-1);
            levelText.text = "LEVEL 2 SCORE: "+levelScore;
        }

        else if (selectedLevel == 3) {
            levelScore = FindObjectOfType<GameManager>().LevelScoreValue(selectedLevel-1);
            levelText.text = "LEVEL 3 SCORE: "+levelScore;
        }
    }

    public void HideText() {
        animator.SetBool("IsSelected", false);
    }
}
