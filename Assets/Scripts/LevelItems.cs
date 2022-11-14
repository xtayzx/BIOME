using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelItems : MonoBehaviour
{
    [SerializeField] private Image item1, item2, item3, star;
    [SerializeField] private GameObject itemOneObject, itemTwoObject, itemThreeObject, starObject, scoreObject;
    public TextMeshProUGUI levelScore;
    public TextMeshProUGUI trashScore;
    private int levelScoreValue;
    private int trashScoreValue;

    //Change the alpha on the items, then set them to false. If these are set to false to start, then the GameObject cannot be changed
    public void ShowItem(int num) {
        if(num == 1) {
            item1.GetComponent<Image>().color = new Color32(255,255,225,255);
            itemOneObject.SetActive(false);
        }

        else if(num == 2) {
            item2.GetComponent<Image>().color = new Color32(255,255,225,255);
            itemTwoObject.SetActive(false);
        }

        else if(num == 3) {
            item3.GetComponent<Image>().color = new Color32(255,255,225,255);
            star.GetComponent<Image>().color = new Color32(255,255,225,255);
            itemThreeObject.SetActive(false);
            starObject.SetActive(false);
        }
    }

    // Set graphics to show on the end level screen to true and change the value of the level score
    public void ShowItemsEnd() {
       itemOneObject.SetActive(true);
       itemTwoObject.SetActive(true);
       itemThreeObject.SetActive(true);
       starObject.SetActive(true);

       levelScoreValue = FindObjectOfType<LevelManager>().TotalLevelScore();
       trashScoreValue = FindObjectOfType<LevelManager>().TotalTrashScore();

       levelScore.text = levelScoreValue.ToString();
       levelScore.overrideColorTags = true;
       levelScore.GetComponent<TextMeshProUGUI>().color = new Color32 (255,255,255,255);

       trashScore.text = trashScoreValue.ToString();
       trashScore.overrideColorTags = true;
       trashScore.GetComponent<TextMeshProUGUI>().color = new Color32 (255,255,255,255);
    }
}
