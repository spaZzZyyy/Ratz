using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection.Emit;
using System.Drawing.Printing;

public class mapMaker : EditorWindow {

    [MenuItem("Tools/Player Tool")]
    private static void ShowWindow() {
        var window = GetWindow<mapMaker>();
        window.titleContent = new GUIContent("Player Tool");
        window.Show();
    }

    void OnEnable() => Selection.selectionChanged += Repaint;
    void OnDisable() => Selection.selectionChanged -= Repaint;

    ScriptMovement scriptMovement;
    Editor sm;
    
    private void OnGUI() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ToolMovement TM = player.GetComponentInChildren<ToolMovement>();
        GUI.Label(new Rect(55, 10, 150, 100), "Player Guide Lines");
        //Enable Movement Guide Lines
        if (GUI.Button(new Rect(10, 70, 100, 20), "Enable")){
            TM.enabled = true;
        }

        //Disable Movement Guide Lines
        if (GUI.Button(new Rect(120, 70, 100, 20), "Disable")){
            TM.enabled = false;
        }

        //Snaps gameobjects to grid
        using (new EditorGUI.DisabledScope(Selection.gameObjects.Length == 0)){
        if (GUI.Button(new Rect(10, 100, 210, 40), "Round Selected Transforms")){
            roundSelection();
        }
        }

    }

    void roundSelection(){
        foreach( GameObject go in Selection.gameObjects){
            Undo.RecordObject(go.transform, "Round Transform" );

            Vector3 scale = go.transform.localScale;
            scale.x = Mathf.Round(scale.x);
            scale.y = Mathf.Round(scale.y);
            scale.z = Mathf.Round(scale.z);
            go.transform.localScale = scale;

            Vector3 pos = go.transform.position;
            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);
            pos.z = Mathf.Round(pos.z);
            go.transform.position = pos;
        }
    }
}