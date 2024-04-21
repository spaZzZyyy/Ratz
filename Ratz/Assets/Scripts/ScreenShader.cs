using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class ScreenShader : MonoBehaviour
{
    Material screenShaderMat;
    [SerializeField] GameObject screenBaby;

    private void OnEnable() {
        Actions.OnPlayerEnterMadness += inMad;
        Actions.OnPlayerExitMadness += outMad;
    }

    private void OnDisable() {
        Actions.OnPlayerEnterMadness -= inMad;
        Actions.OnPlayerExitMadness -= outMad;
    }

    void Start(){
        //screenShaderMat = GetComponent<Material>();
        screenBaby.SetActive(false);
    }

    void inMad(){
        //screenShaderMat.SetInt("_isMad", 0); // 0 is true
        screenBaby.SetActive(true);
    }
    void outMad(){
        //screenShaderMat.SetInt("_isMad", 1);
        screenBaby.SetActive(false);
    }
}
