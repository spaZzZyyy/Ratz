using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CameraTool : EditorWindow
{

    [MenuItem("Tools/Camera Tool")]
    private static void ShowWindow(){
        var window = GetWindow<CameraTool>();
        window.titleContent = new GUIContent("Camera Tool");
        window.Show();
    }

    void OnEnable() => Selection.selectionChanged += Repaint;
    void OnDisable() => Selection.selectionChanged -= Repaint;

    camControls camControls;
    Editor CC;
    public float scrollPos = 110;
    public GameObject camBody;
    
    private void OnGUI() {
        GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        ToolCamera TC = mainCam.GetComponent<ToolCamera>();

        GUI.Label(new Rect(57, 10, 150, 100), "Camera Zoom");
        scrollPos = GUI.HorizontalScrollbar(new Rect(48, 90, 100, 20), scrollPos, 0, TC.camControls.zoomMin, TC.camControls.zoomMax);
        camBody = GameObject.FindGameObjectWithTag("MainCamera");
        Camera cam = camBody.GetComponent<Camera>();
        cam.orthographicSize = scrollPos;
        EditorGUI.FloatField(new Rect(75, 70, 50, 20), scrollPos);
    }
}
