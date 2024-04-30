using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] CameraFollow cameraFollow;

    private void OnTriggerEnter2D(Collider2D camSwitch){
        if(camSwitch.gameObject.CompareTag("cameraMove")) {
           if(cameraFollow.i < cameraFollow.points.Length) {
                cameraFollow.target = cameraFollow.points[cameraFollow.i];
           }
           // Destroy(camSwitch.gameObject);
        } if(camSwitch.gameObject.CompareTag("cameraReset")) {
            cameraFollow.target = cameraFollow.player;
            cameraFollow.i++; 
            cameraFollow.zoomSize = 16;
            Destroy(camSwitch.gameObject);
        } if(camSwitch.gameObject.CompareTag("cameraBounds")) {
            cameraFollow.target = cameraFollow.player;
        }

    }
}
