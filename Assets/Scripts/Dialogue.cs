using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue {
    public string name; //Name of NPC

    [TextArea(3,10)] //How much text for each sentence
    public string[] sentences;

    [SerializeField] public Sprite sprite; // NPC image
}
