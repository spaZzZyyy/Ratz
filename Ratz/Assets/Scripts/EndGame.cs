using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnEnable() {
        Actions.OnGameEnd += StartEndGame;
    }

    void OnDisable(){
        Actions.OnGameEnd -= StartEndGame;
    }

    void StartEndGame(){
        SceneManager.LoadScene("EndScene");
    }
}
