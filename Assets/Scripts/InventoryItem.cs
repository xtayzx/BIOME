using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;

    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;

    private void Start() {
        InitialiseItem(item);
    }

    //If the item is stackable, then show text in the inventory
    public void RefreshCount() {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    //Refresh how many of that item is in the inventory
    public void InitialiseItem(Item newItem) {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }
}
