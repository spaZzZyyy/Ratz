using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class mouseTail : MonoBehaviour
{
    private float _playerThickness;
    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    public Transform targetDir;
    public float targetDistance;
    public float smoothSpeed;
    public float trailSpeed;
    public float trailSpeedRun;
    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;
    public float wigRun;
    private float wigFactor;
    float trailSpeedFactor;
    private Vector3[] segmentVelocity;
    public float tailEndHeight;
    private Transform tarDirTransform;
    // Start is called before the first frame update
    void Start()
    {
        _playerThickness = transform.localScale.x;

        tarDirTransform = GetComponentInChildren<Transform>();
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentVelocity = new Vector3[length];
        trailSpeedFactor = trailSpeed;
        wigFactor = wiggleMagnitude;
    }

    // Update is called once per frame
    void Update()
    {
        #region ->Used BlackThornProd trail design
        wiggleDir.localRotation = Quaternion.Euler(0,tailEndHeight,Mathf.Sin(Time.time * wiggleSpeed) * wigFactor);
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++){
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i-1] + targetDir.right * targetDistance, ref segmentVelocity[i], smoothSpeed + i / trailSpeedFactor);
        }
        lineRend.SetPositions(segmentPoses);
        #endregion

        flipTail();
        
    }
    void flipTail(){
        wigFactor = wiggleMagnitude;
        trailSpeedFactor = trailSpeed;
        if(Input.GetKey(KeyCode.A)){
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

        if(Input.GetKey(KeyCode.D)){
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
}
