using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    // public TileBase tile; //show graphic
    public ItemType type;
    public ActionType actionType;
    // public Vector2Int range = new Vector2Int(5,4);

    [Header("Only UI")]
    public bool stackable; //bucket is the only exception of being a stackable item

    [Header("Both")]
    public Sprite image; //sprite in inventory
}


public enum ItemType {
    Bucket,
    Shovel,
    Apple
}

public enum ActionType {
    Tool,
    Food
}