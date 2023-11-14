using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CamFocus : MonoBehaviour
{
    Camera cam;
    [SerializeField] camControls camControls;
    [SerializeField] ScriptControls scriptControls;
    [SerializeField] Transform focusTransform;
    private bool zoomOut;
    private bool disrupt;
    Vector3 camStartPos;
    void Start()
    {
        cam = GetComponent<Camera>();
        camStartPos = transform.position;
    }

    void Update()
    {
        CheckMovement();
    }

    void CheckMovement(){
        StartCoroutine(StartZoomOut());
        if(Input.GetKey(scriptControls.moveLeft) || Input.GetKey(scriptControls.moveRight) || Input.GetKey(scriptControls.moveJump))
        {
            ZoomOut();
            StopCoroutine(StartZoomOut());
            zoomOut = false;
        } else
        {
            disrupt = false;
            if(zoomOut == true){
                ZoomIn();
                zoomOut = false;
            }
        }
    }

    void ZoomOut(){
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camControls.camZoomOutSize, camControls.camZoomSpeed);
        transform.position = Vector3.Lerp(focusTransform.position, camStartPos, camControls.camZoomSpeed);
    }

    void ZoomIn(){
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camControls.camZoomInSize, camControls.camZoomSpeed);
        transform.position = Vector3.Lerp(camStartPos, focusTransform.position, camControls.camZoomSpeed);
    }

    IEnumerator StartZoomOut(){
        yield return new WaitForSeconds(camControls.timeUntilZoomOut);
        if(disrupt == false){
            zoomOut = true;
        }
    }


}