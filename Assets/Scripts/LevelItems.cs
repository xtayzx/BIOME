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
    public TextMeshProUGUI didYouKnowText;
    private int levelScoreValue;
    private int trashScoreValue;
    private int gameState;

    void Awake() {
        gameState = FindObjectOfType<GameManager>().GetActiveLevel();
    }

    void Start() {
        //If at level 1
        if(gameState == 1) {
            didYouKnowText.text = "Approximately 30,000 species per year — about three per hour — are being driven to extinction. Approximately 80 percent of the decline in global biological diversity is caused by habitat destruction. Wildlife habitat in the world is being destroyed at a rate of approximately 5,760 acres per day or 240 acres per hour. (Harvard University, African Conservancy)";
        }

        //If at level 2
        if(gameState == 2) {
            didYouKnowText.text = "Rising temperatures, a key indicator of climate change, evaporate more moisture from the ground, drying out the soil, and making vegetation more flammable. At the same time, winter snowpacks are melting about a month earlier, meaning that the forests are drier for longer periods of time. (Environmental Defense Fund)";
        }

        //If at level 3
        if(gameState == 3) {
           didYouKnowText.text = "Oil destroys the insulating ability of fur-bearing mammals, such as sea otters, and the water repellency of a bird's feathers, thus exposing these creatures to the harsh elements. Without the ability to repel water and insulate from the cold water, birds and mammals will die from hypothermia. (National Ocean Service)";
        }
    }

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

       didYouKnowText.overrideColorTags = true;
       didYouKnowText.GetComponent<TextMeshProUGUI>().color = new Color32 (255,255,255,255);
    }
}
