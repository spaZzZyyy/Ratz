using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class mouseTail : MonoBehaviour
{
    PlayerMovement player_movement;
    [SerializeField] ScriptControls scriptControls;
    private float _playerThickness;
    public int length;
    public LineRenderer lineRend;
    [HideInInspector] public Vector3[] segmentPoses;
    public Transform targetDir;
    public float targetDistance;
    public float smoothSpeed;
    public float trailSpeed;
    public float trailSpeedRun;
    public float wiggleSpeed;
    public float wiggleMagnitude;
    public float jumpWig;
    public float jumpTailSpeed;
    public Transform wiggleDir;
    public float wigRun;
    private float wigFactor;
    float trailSpeedFactor;
    private Vector3[] segmentVelocity;
    public float tailEndHeight;
    private Transform tarDirTransform;
    private LineRenderer tail;
    // Start is called before the first frame update
    void Start()
    {
        tail = GetComponent<LineRenderer>();
        tail.enabled = true;
        _playerThickness = transform.localScale.x;
        tarDirTransform = GetComponentInChildren<Transform>();
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentVelocity = new Vector3[length];
        trailSpeedFactor = trailSpeed;
        wigFactor = wiggleMagnitude;
        player_movement = GetComponentInParent<PlayerMovement>();
        ResetTail();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
            problem TO FIX,
            The mouse tail isn't seeable when flipped, not able to see due to Z axis being positive
            need to change something so the tail render segments Z axis remains negative
        */
        #region ->Used BlackThornProd trail design
            wiggleDir.localRotation = Quaternion.Euler(0,tailEndHeight,Mathf.Sin(Time.time * wiggleSpeed) * wigFactor);
            segmentPoses[0] = targetDir.position;

            for (int i = 1; i < segmentPoses.Length; i++){
                segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i-1] + targetDir.right * targetDistance, ref segmentVelocity[i], smoothSpeed + i / trailSpeedFactor);
                
            }
            lineRend.SetPositions(segmentPoses);
        #endregion

        FlipTail();

        if (player_movement.IsGrounded() == false){
            ResetTail();
        }

    }
    void FlipTail(){
        wigFactor = wiggleMagnitude;
        trailSpeedFactor = trailSpeed;
        
        if(Input.GetKey(scriptControls.moveLeft))
        {
            FlipTailLeft();
        }
        if(Input.GetKey(scriptControls.moveRight)){
            FlipTailRight();
        }
    }

    void ResetTail(){
        wigFactor = jumpWig;
        trailSpeedFactor = jumpTailSpeed;

        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < length; i++){
            segmentPoses[i] = segmentPoses[i - 1] + targetDir.right * targetDistance;
        }
        lineRend.SetPositions(segmentPoses);

    }
    void FlipTailLeft(){
        Vector2 localScale = transform.localScale;
        localScale.x = _playerThickness;
        transform.localScale = localScale;

        transform.localRotation = Quaternion.Euler(0, 180, 0);

        Vector2 childLocalScale = tarDirTransform.localScale;
        childLocalScale.x = _playerThickness;
        tarDirTransform.localScale = childLocalScale;

        trailSpeedFactor = trailSpeedRun;
        wigFactor = wigRun;
    }

    void FlipTailRight(){
        Vector2 localScale = transform.localScale;
        localScale.x = -_playerThickness;
        transform.localScale = localScale;

        transform.localRotation = Quaternion.Euler(0, 0, 0);

        Vector2 childLocalScale = tarDirTransform.localScale;
        childLocalScale.x = -_playerThickness;
        tarDirTransform.localScale = childLocalScale;

        trailSpeedFactor = trailSpeedRun;
        wigFactor = wigRun;
    }

}
