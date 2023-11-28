using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject[] camPositions;
    private bool switcher = false;
    private bool switcherLast = false;
    private int camRollSpot = 0;
    private Vector3[] camRoll;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        convertTransform();
    }

    private void OnEnable() {
        Actions.OnCameraSwitchTrigger += CameraSwitch;
    }

    private void OnDisable() {
        Actions.OnCameraSwitchTrigger -= CameraSwitch;
    }

    private void Update() {
        CameraSwitch();
        Debug.Log(camRollSpot);
    }

    private void CameraSwitch()
    {
        if (switcher == true){
            camRollSpot++;
        }

        if (switcher == false && switcherLast == true){
            camRollSpot--;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            switcher = true;
            switcherLast = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            switcher = false;
        }
    }

//Take transform from camPositions and convert to vector 3
    void convertTransform(){
        for (int i = 0; i < camPositions.Length - 1; i++){
            camRoll[i] = camPositions[i].transform.position;
        }
    }

    void updateCamPos(){
        cam.transform.position = camRoll[camRollSpot];
    }
}
