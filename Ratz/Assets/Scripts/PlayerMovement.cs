using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    public ScriptMovement scriptMovement;
    public ScriptControls controls;
    [SerializeField] private Transform groundCheck;
    private Rigidbody2D _playerRigidbody;
    private float _movementPlayer;
    private float _playerThickness;
    private bool _canDash = true;
    private bool _canJump = true;
    private int _maxJumps = 1;
    private int _numJumps = 0;

    private float _timeFromGround;

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
            if (Input.GetKey(_moveRightButton))
            {
                _movementPlayer = 1f;
                Vector2 localScale = transform.localScale;
                localScale.x = _playerThickness;
                transform.localScale = localScale;
            }

            if (Input.GetKey(_moveLeftButton))
            {
                _movementPlayer = -1f;
                Vector2 localScale = transform.localScale;
                localScale.x = -_playerThickness;
                transform.localScale = localScale;
            }

            #endregion

        #region Jump
            if ( (Input.GetKeyDown(_moveJumpButton) && IsGrounded() ) || (Input.GetKeyDown(_moveJumpButton) && (scriptMovement.coyoteTime > _timeFromGround) && _canJump))
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, scriptMovement.jumpForce);
                _numJumps++;
                Actions.OnPlayerJump();
            }

            if (Input.GetKeyUp(_moveJumpButton) && _playerRigidbody.velocity.y > 0f && _canJump)
            {
                _numJumps++;
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _playerRigidbody.velocity.y * scriptMovement.minJumpHeight);
                Actions.OnPlayerJump();
            }
            
        #endregion
        
        #region Dash
        
        if (Input.GetKey(_moveDashButton))
        {
            if (_canDash && _playerRigidbody.velocity.x !=0)
            {
                _playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
                _playerRigidbody.AddForce(new Vector2(scriptMovement.dashDistance * _movementPlayer, 0));
                StartCoroutine(OnDash());
            }
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
            _numJumps = 0;
            _timeFromGround = 0;
        }

        //Jump Cap
        if(_numJumps <= _maxJumps)
        {
            _canJump = true;
        } else 
        {
            _canJump = false;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, scriptMovement.groundCheckDistance, scriptMovement.groundLayer);
    }
    
    
    IEnumerator OnDash()
    {
        
        yield return new WaitForSeconds(scriptMovement.dashDuration);
        _canDash = false;
        _playerRigidbody.constraints = RigidbodyConstraints2D.None;
        _playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(scriptMovement.dashCoolDown);
        _canDash = true;
    }
}
