using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TreeLevel : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;

    public GameObject tree4;
    public GameObject tree5;
    public GameObject tree6;

    public GameObject Bear1;
    public GameObject Bear2;
    public GameObject Bear3;

    public GameObject Duckling1;
    public GameObject Duckling2;
    public GameObject Duckling3;

    public GameObject deer;
    public GameObject bobcat;
    public GameObject rabbit;

    public GameObject MamaBear;
    public GameObject MamaDuck;

    private int completedLevel;

    void Awake() {
        completedLevel = FindObjectOfType<GameManager>().CompletedLevelValue();
    }
    
    void Update()
    {
        //Keeping them in for programming otherwise I will forget they exist
        if(completedLevel == 0) {
            Bear1.SetActive(false);
            Bear2.SetActive(false);
            Bear3.SetActive(false);

            deer.SetActive(false);
            bobcat.SetActive(false);

            Duckling1.SetActive(false);
            Duckling2.SetActive(false);
            Duckling3.SetActive(false);
        }

        if(completedLevel == 1) {
            tree1.SetActive(false);
            tree2.SetActive(false);
            tree3.SetActive(false);

            deer.SetActive(false);
            bobcat.SetActive(false);

            Bear1.SetActive(true);
            Bear2.SetActive(true);
            Bear3.SetActive(true);

            Duckling1.SetActive(false);
            Duckling2.SetActive(false);
            Duckling3.SetActive(false);
        }

        else if(completedLevel == 2) {
            tree1.SetActive(false);
            tree2.SetActive(false);
            tree3.SetActive(false);

            tree4.SetActive(false);
            tree5.SetActive(false);
            tree6.SetActive(false);

            deer.SetActive(true);
            bobcat.SetActive(true);

            Bear1.SetActive(true);
            Bear2.SetActive(true);
            Bear3.SetActive(true);

            Duckling1.SetActive(false);
            Duckling2.SetActive(false);
            Duckling3.SetActive(false);
        }

        //  All levels complete
        else if(completedLevel == 3) {
            tree1.SetActive(false);
            tree2.SetActive(false);
            tree3.SetActive(false);

            tree4.SetActive(false);
            tree5.SetActive(false);
            tree6.SetActive(false);

            Bear1.SetActive(true);
            Bear2.SetActive(true);
            Bear3.SetActive(true);

            deer.SetActive(true);
            bobcat.SetActive(true);

            Duckling1.SetActive(true);
            Duckling2.SetActive(true);
            Duckling3.SetActive(true);
        }
    }
}
