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
    private int levelScoreValue;

    // public void Start() {
    //     star.GetComponent<Image>().color = new Color32(0,0,0,100);
    //     item1.GetComponent<Image>().color = new Color32(0,0,0,100);
    //     item2.GetComponent<Image>().color = new Color32(0,0,0,100);
    //     item3.GetComponent<Image>().color = new Color32(0,0,0,100);
    // }

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

    public void ShowItemsEnd() {
       itemOneObject.SetActive(true);
       itemTwoObject.SetActive(true);
       itemThreeObject.SetActive(true);
       starObject.SetActive(true);

       levelScoreValue = FindObjectOfType<Timer>().Score();
       levelScore.text = levelScoreValue.ToString();
       levelScore.overrideColorTags = true;
       levelScore.GetComponent<TextMeshProUGUI>().color = new Color32 (255,255,255,255);

        // star.GetComponent<Image>().color = new Color32(star.color.r,star.color.g,star.color.b,100);
        // item1.GetComponent<Image>().color = new Color32(item1.color.r,item1.color.g,item1.color.b,100);
        // item2.GetComponent<Image>().color = new Color32(item2.color.r,item2.color.g,item2.color.b,100);
        // item3.GetComponent<Image>().color = new Color32(item3.color.r,item3.color.g,item3.color.b,100);
    }
}
