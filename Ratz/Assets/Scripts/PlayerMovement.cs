using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ScriptMovement scriptMovement;
    public ScriptControls controls;
    
    [SerializeField] private Transform groundCheck;
    private Rigidbody2D _playerRigidbody;
    private Animator _playerAni;
    
    private float _movementPlayer;
    private float _playerThickness;
    private bool _canDash = true;

    private float _timeFromGround;

    #region Assigning Controls

        private KeyCode _moveRightButton;
        private KeyCode _moveLeftButton;
        private KeyCode _moveJumpButton;
        private KeyCode _moveCrouchButton;
        private KeyCode _atkShootButton;
        private KeyCode _atkRotateInvRightButton;
        private KeyCode _atkRotateInvLeftButton;
        private KeyCode _moveDashButton;

    #endregion


    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerThickness = transform.localScale.x;
        _playerAni = GetComponent<Animator>();

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
            if ( (Input.GetKeyDown(_moveJumpButton) && IsGrounded() ) || (Input.GetKeyDown(_moveJumpButton) && scriptMovement.coyoteTime > _timeFromGround))
            {
                Debug.Log(_timeFromGround);
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, scriptMovement.jumpForce);
            }

            if (Input.GetKeyUp(_moveJumpButton) && _playerRigidbody.velocity.y > 0f)
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _playerRigidbody.velocity.y * scriptMovement.minJumpHeight);
            }
            
        #endregion
        
        #region Dash
        
        if (Input.GetKey(_moveDashButton))
        {
            if (_canDash)
            {
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
            _timeFromGround++;
        }
        else
        {
            _timeFromGround = 0;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, scriptMovement.groundCheckDistance, scriptMovement.groundLayer);
    }
    
    
    IEnumerator OnDash()
    {
        
        yield return new WaitForSeconds(scriptMovement.dashDuration);
        _canDash = false;
        yield return new WaitForSeconds(scriptMovement.dashCoolDown);
        _canDash = true;
    }
}
