using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutScene : MonoBehaviour
{
    void OnEnable() {
        SceneManager.LoadScene("Menu");
    }
}
