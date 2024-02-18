using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    public ScriptMovement scriptMovement;
    public ScriptControls controls;
    [SerializeField] private Transform groundCheck;
    private Rigidbody2D _playerRigidbody;
    private float _movementPlayer;
    private float _playerThickness;
    private bool _canDash = true;
    private bool _keepZLocked = true;
    private int jumpCount = 0;
    [HideInInspector] public bool isFalling;
    BoxCollider2D box;
    private float _timeFromGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float playerKnockbackX;

    [SerializeField] private float playerKnockbackY;


    #region Assigning Controls

    private KeyCode _moveRightButton;
    private KeyCode _moveLeftButton;
    private KeyCode _moveJumpButton;
    private KeyCode _moveCrouchButton;
    private KeyCode _moveDashButton;

    #endregion


    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerThickness = transform.localScale.x;
        box = GetComponent<BoxCollider2D>();   
        #region Assigning Controls

            _moveRightButton = controls.moveRight;
            _moveLeftButton = controls.moveLeft;
            _moveJumpButton = controls.moveJump;
            _moveCrouchButton = controls.moveCrouch;
            _moveDashButton = controls.dash;

        #endregion

    }

    private void Update()
    {
        #region Run Input
            _movementPlayer = 0f;

            #region FlipSprite
                if (Input.GetKey(_moveLeftButton))
                {
                    _movementPlayer = -1f;
                    Vector2 localScale = transform.localScale;
                    localScale.x = -_playerThickness;
                    transform.localScale = localScale;
                }

                if (Input.GetKey(_moveRightButton))
                {
                    _movementPlayer = 1f;
                    Vector2 localScale = transform.localScale;
                    localScale.x = _playerThickness;
                    transform.localScale = localScale;
                }
            #endregion

            #endregion

        #region Jump
            if ( (Input.GetKeyDown(_moveJumpButton) && (IsGrounded() || (scriptMovement.coyoteTime > _timeFromGround)) && jumpCount < scriptMovement.numJumps))
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, scriptMovement.jumpForce);
            }

            if (Input.GetKeyUp(_moveJumpButton) && _playerRigidbody.velocity.y > 0f)
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _playerRigidbody.velocity.y * scriptMovement.minJumpHeight);
            }

            if (Input.GetKeyDown(_moveJumpButton) || Input.GetKeyUp(_moveJumpButton)){
                jumpCount++;
                Actions.OnPlayerJump();
            }
            
        #endregion
        
        #region Dash
        
        if (Input.GetKey(_moveDashButton))
        {
            Dash();
        }

        #endregion

    
    }

    private void FixedUpdate()
    {
        _playerRigidbody.gravityScale = scriptMovement.gravityForce;
        #region Movement
        //Run
            _playerRigidbody.velocity = new Vector2(_movementPlayer * scriptMovement.movementSpeed, _playerRigidbody.velocity.y);
            
        #endregion

        //Coyote time
        if (IsGrounded() == false)
        {
            _timeFromGround+=Time.deltaTime;
        }
        else
        {
            jumpCount = 0;
            _timeFromGround = 0;
        }

        if (_keepZLocked){
            _playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        //to Fix mouse tail extending when sliding down slanted platforms
        if (_playerRigidbody.velocity.y == 0){
            isFalling = false;
        } else {
            isFalling = true;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D rayHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rayHit.collider != null;
    }


    
    void Dash(){
        if (_canDash && _playerRigidbody.velocity.x !=0)
            {
                Actions.OnPlayerDashed();
                //_playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
                _playerRigidbody.AddForce(new Vector2(scriptMovement.dashDistance * _movementPlayer, -30));
                StartCoroutine(OnDash());
            }
    }

    public void Hit(Vector2 dir)
    {
        Vector2 hitDir = new Vector2(dir.x * playerKnockbackX, playerKnockbackY) * _playerRigidbody.mass;
        _playerRigidbody.AddForce(hitDir);
    }

    IEnumerator OnDash()
    {
        
        yield return new WaitForSeconds(scriptMovement.dashDuration);
        _canDash = false;
        _playerRigidbody.constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(scriptMovement.dashCoolDown);
        _canDash = true;
    }
}
