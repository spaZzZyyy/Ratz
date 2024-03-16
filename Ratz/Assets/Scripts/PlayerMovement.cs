using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    public ScriptMovement scriptMovement;
    public ScriptControls controls;
    [SerializeField] private Transform groundCheck;
    private Rigidbody2D _playerRigidbody;
    private float _movementPlayer;
    private float _playerThickness;
    [HideInInspector] public bool _canDash = true; //Used in dash particles
    public bool _dashing = false;
    private bool _keepZLocked = true;
    private int jumpCount = 0;
    [HideInInspector] public bool isFalling;
    BoxCollider2D box;
    private float _timeFromGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float playerKnockbackX;

    [SerializeField] private float playerKnockbackY;
    public PlayerControls playerControls;





    #region Assigning Controls

    private InputAction jump;
    private bool heldJump;
    private InputAction MoveLeft;
    private bool heldLeft;
    private InputAction MoveRight;
    private bool heldRight;
    private InputAction DashButton;

    private void OnEnable()
    {
        jump = playerControls.Gameplay.Jump;
        jump.Enable();
        jump.performed += Jump;

        MoveLeft = playerControls.Gameplay.MoveLeft;
        MoveLeft.Enable();
        MoveLeft.performed += RunLeft;

        MoveRight = playerControls.Gameplay.MoveRight;
        MoveRight.Enable();
        MoveRight.performed += RunRight;

        DashButton = playerControls.Gameplay.Dash;
        DashButton.Enable();
        DashButton.performed += Dash;

    }

    private void OnDisable()
    {
        jump.Disable();
        MoveLeft.Disable();
        MoveRight.Disable();
        DashButton.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    #endregion


    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerThickness = transform.localScale.x;
        box = GetComponent<BoxCollider2D>();   
    }

    private void Update()
    {
        heldJump = playerControls.Gameplay.Jump.ReadValue<float>() > 0;
        heldLeft = playerControls.Gameplay.MoveLeft.ReadValue<float>() > 0;
        heldRight = playerControls.Gameplay.MoveRight.ReadValue<float>() > 0;

        if (!heldLeft && !heldRight){
            _movementPlayer = 0;
        }

        if ((!heldJump) && _playerRigidbody.velocity.y > 0f)
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _playerRigidbody.velocity.y * scriptMovement.minJumpHeight);
        }
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

    public void Jump(InputAction.CallbackContext ctx)
    {
        if ((ctx.performed && (IsGrounded() || (scriptMovement.coyoteTime > _timeFromGround)) && jumpCount < scriptMovement.numJumps))
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, scriptMovement.jumpForce);
        }
        if (ctx.performed)
        {
            jumpCount++;
            /*Actions.OnPlayerJump();*/
        }
    }

    public void RunLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.performed || heldLeft)
        {
            
            _movementPlayer = -1f;
            Vector2 localScale = transform.localScale;
            localScale.x = -_playerThickness;
            transform.localScale = localScale;
        }
    }

    public void RunRight(InputAction.CallbackContext ctx)
    {
        if (ctx.performed || heldRight)
        {
            _movementPlayer = 1f;
            Vector2 localScale = transform.localScale;
            localScale.x = _playerThickness;
            transform.localScale = localScale;
        } 
    }



    public void Dash(InputAction.CallbackContext ctx)
    {
        
        if (ctx.performed && _canDash && _playerRigidbody.velocity.x !=0)
            {
            Debug.Log("dashed");
                
                _playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
                _playerRigidbody.AddForce(new Vector2(scriptMovement.dashDistance * _movementPlayer, -30),ForceMode2D.Impulse);
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
        _dashing = true;
        _playerRigidbody.constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(scriptMovement.dashCoolDown);
        _canDash = true;
        _dashing = false;
    }
}
