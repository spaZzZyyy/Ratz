using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ToolMovement : MonoBehaviour
{
    /*
    A tool script used to help determine player movement for default controls variables
    */
    Transform playerTrans;
    bool isRunning;
    public ScriptMovement scriptMovement;

    void OnEnable() {
        isRunning = true;
    }

    private void OnDisable() {
        isRunning = false;
    }

    void OnDrawGizmos(){
        if (isRunning == true){
        playerTrans = GetComponent<Transform>();
        #region Jumping Gizmos

            #region MaxJumpHeight
                Gizmos.color = Color.blue;
                Vector3 playerJumpHeight = new Vector3(playerTrans.position.x, playerTrans.position.y + scriptMovement.jumpForce * 0.36f);
                Gizmos.DrawLine(playerTrans.position, playerJumpHeight);
                Gizmos.DrawLine(playerJumpHeight, new Vector3(playerJumpHeight.x + 7f, playerJumpHeight.y));
                Gizmos.DrawLine(playerJumpHeight, new Vector3(playerJumpHeight.x - 7f, playerJumpHeight.y));
            #endregion

            #region MinJumpHeight
                Gizmos.color = Color.red;
                Vector3 minJumpHeight = new Vector3(playerTrans.position.x, playerTrans.position.y + scriptMovement.minJumpHeight * 30f);
                Gizmos.DrawLine(playerTrans.position, minJumpHeight);
                Gizmos.DrawLine(minJumpHeight, new Vector3(minJumpHeight.x + 10f, minJumpHeight.y));
                Gizmos.DrawLine(minJumpHeight, new Vector3(minJumpHeight.x - 10f, minJumpHeight.y));
            #endregion

        #endregion

        #region Dash Gizmo
        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(new Vector3(playerTrans.position.x, playerTrans.position.y + 10f),
        new Vector3(playerTrans.position.x + scriptMovement.dashDistance * 0.031f, playerTrans.position.y + 10f));

        Gizmos.DrawLine(new Vector3(playerTrans.position.x, playerTrans.position.y + 10f),
        new Vector3(playerTrans.position.x - scriptMovement.dashDistance * 0.031f, playerTrans.position.y + 10f));
        #endregion
        }
    }

}
