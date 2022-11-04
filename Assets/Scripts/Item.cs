using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")] //Create an Item object in Unity
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public ItemType type;
    public ActionType actionType;

    [Header("Only UI")]
    public bool stackable; //bucket is the only exception of being a stackable item

    [Header("Both")]
    public Sprite image; //sprite in inventory
}

public enum ItemType {
    Bucket,
    Shovel,
    Apple,
    OilCleaner,
    SaveDuck,
    Trash
}

public enum ActionType {
    Tool,
    Food,
    Trash
}