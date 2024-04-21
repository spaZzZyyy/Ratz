using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private LineRenderer tailRun;
    private bool playerMoving;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform recordPlayerTransform;
    bool match = false;
    bool firstTime = false;
    public PlayerControls playerControls;
    private InputAction MoveLeft;
    private bool heldLeft;
    private InputAction MoveRight;
    private bool heldRight;
    bool playerRunning;
    [SerializeField] GameObject tailRunGO;
    [SerializeField] GameObject tailNormal;
    bool alreadyReset = false;
    private float smoothSpeedRecord;
    


    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        MoveLeft = playerControls.Gameplay.MoveLeft;
        MoveLeft.Enable();
        MoveLeft.performed += FlipTailLeft;

        MoveRight = playerControls.Gameplay.MoveRight;
        MoveRight.Enable();
        MoveRight.performed += FlipTailRight;
    }

    private void OnDisable()
    {

        MoveLeft.Disable();
        MoveRight.Disable();
    }

    void Start()
    {
        recordPlayerTransform.position = playerTransform.position;
        tail = tailNormal.GetComponent<LineRenderer>();
        tailRun = tailRunGO.GetComponent<LineRenderer>();
        tail.enabled = true;
        tailRun.enabled = false;
        _playerThickness = transform.localScale.x;
        tarDirTransform = GetComponentInChildren<Transform>();
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentVelocity = new Vector3[length];
        trailSpeedFactor = trailSpeed;
        wigFactor = wiggleMagnitude;
        player_movement = GetComponentInParent<PlayerMovement>();
        smoothSpeedRecord = smoothSpeed;
        ResetTail();
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

        if(playerTransform.position == recordPlayerTransform.position){
            match = true;
        } else{
            match = false;
        }
        playerRunning = playerControls.Gameplay.MoveLeft.ReadValue<float>() > 0 || playerControls.Gameplay.MoveRight.ReadValue<float>() > 0;
        /*
        nom
        12
        -0.06
        0.2
        300
        2000
        4.44
        30
        5
        2
        6.38
        5.71
        run
        4
        -0.06
        0.0001
        70
        2000
        4.44
        30
        5
        2
        6.38
        5.71
        */
        if (playerRunning) { 
            
            playerMoving = false;
            tailRun.enabled = true;
            tail.enabled = false;
        }else
        {
            ResetTail();
            tailRun.enabled = false;
            tail.enabled = true;
            if(player_movement.isFalling == false){
                recordPlayerTransform.position = playerTransform.position;
            }
        }

        if(player_movement.IsGrounded() && firstTime == true){
            StartCoroutine("JustLanded");
        }

        if(playerMoving == false){ 
            if(match == false){
                ResetTail();
            }
        }

//isFalling is in playerMovement just checks if velocity.y is 0

        if(player_movement.IsGrounded() == false){
            firstTime = true;
        }

        //Debug.Log(match);
    }

    IEnumerator JustLanded(){
        yield return new WaitForSeconds(0.3f);
        recordPlayerTransform.position = playerTransform.position;
        firstTime = false;
    }

    void FixedUpdate()
    {
        #region ->Used BlackThornProd trail design
            wiggleDir.localRotation = Quaternion.Euler(0,tailEndHeight,Mathf.Sin(Time.time * wiggleSpeed) * wigFactor);
            segmentPoses[0] = targetDir.position;

            for (int i = 1; i < segmentPoses.Length; i++){
                segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i-1] + targetDir.right * targetDistance, ref segmentVelocity[i], smoothSpeed + i / trailSpeedFactor);
                
            }
            lineRend.SetPositions(segmentPoses);
        #endregion


        if(match == false){
            if (this.gameObject == tailNormal.gameObject){
                smoothSpeed = -1;
            }
        }

        if(match == true){
            if (this.gameObject == tailNormal.gameObject){
                smoothSpeed = smoothSpeedRecord;
            }
        }
        FlipTail();

/*
        if(player_movement.isFalling || player_movement.IsGrounded()){
            ResetTail();
        }
        */

    }
    void FlipTail(){
        wigFactor = wiggleMagnitude;
        trailSpeedFactor = trailSpeed;
    }

    void ResetTail(){
        /*
        if(match == false){
            wigFactor = jumpWig;
            trailSpeedFactor = jumpTailSpeed;

            segmentPoses[0] = targetDir.position;

            for (int i = 1; i < length; i++){
                segmentPoses[i] = segmentPoses[i - 1] + targetDir.right * targetDistance;
            }
            lineRend.SetPositions(segmentPoses);
        }
        */
        

        if (match == true){
            if (alreadyReset == false)
            {
                wigFactor = jumpWig;
                trailSpeedFactor = jumpTailSpeed;

                segmentPoses[0] = targetDir.position;

                for (int i = 1; i < length; i++){
                    segmentPoses[i] = targetDir.position;
                }

                for (int i = 1; i < length; i++){
                    segmentPoses[i] = segmentPoses[i - 1] + targetDir.right * targetDistance;
                }
                lineRend.SetPositions(segmentPoses);

                alreadyReset = true;
            }
        } else {
            alreadyReset = false;
        }
    }
    void FlipTailLeft(InputAction.CallbackContext ctx){
        if (ctx.performed)
        {
            Vector2 localScale = transform.localScale;
            localScale.x = _playerThickness;
            transform.localScale = localScale;

            transform.localRotation = Quaternion.Euler(0, 180, 0);

            Vector2 childLocalScale = tarDirTransform.localScale;
            childLocalScale.x = _playerThickness;
            tarDirTransform.localScale = childLocalScale;

            trailSpeedFactor = trailSpeedRun;
            wigFactor = wigRun;
            playerMoving = true;
            recordPlayerTransform.position = playerTransform.position;
        }
    }

    void FlipTailRight(InputAction.CallbackContext ctx){
        if (ctx.performed)
        {
            Vector2 localScale = transform.localScale;
            localScale.x = -_playerThickness;
            transform.localScale = localScale;

            transform.localRotation = Quaternion.Euler(0, 0, 0);

            Vector2 childLocalScale = tarDirTransform.localScale;
            childLocalScale.x = -_playerThickness;
            tarDirTransform.localScale = childLocalScale;

            trailSpeedFactor = trailSpeedRun;
            wigFactor = wigRun;
            playerMoving = true;
            recordPlayerTransform.position = playerTransform.position;
        }
    }

}
