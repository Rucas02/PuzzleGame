using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Components")]
    private PlayerControls _inputActions;
    private Player _player;

    [Header("Movement")]
    private float _horizontal;
    private float _vertical;
    private float _moveAmount;

    private bool _jumpInput;
    private bool _grabInput;
    private bool _swapRightInput;
    private bool _swapLeftInput;
    private bool _actionInput;

    private Vector2 _movementInput;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {

        HandleInput();
        
        //_player.Move(new Vector3(horizontal, vertical));
        
        
        //if (swap_Input) _player.SwapPlayer();
        
        
        
        if(_actionInput) _player.Action();
        _player.UpdateAnimationValues(_horizontal, _vertical);
    }

    private void FixedUpdate()
    {
        _player.Move(new Vector3(_horizontal, _vertical));
    }

    private void LateUpdate()
    {

        _jumpInput = false;
        _grabInput = false;

        _swapLeftInput = false;
        _swapRightInput = false;

        _actionInput = false;
    }

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerControls();
            _inputActions.PlayerMovements.Movement.performed += inputActions => _movementInput = inputActions.ReadValue<Vector2>();

            _inputActions.PlayerActions.Jump.performed += i => _jumpInput = true;
            _inputActions.PlayerActions.Grab.performed += i => _grabInput = true;
            _inputActions.PlayerActions.SwapL.performed += i => _swapLeftInput = true;
            _inputActions.PlayerActions.SwapR.performed += i => _swapRightInput = true;
            _inputActions.PlayerActions.Action.performed += i => _actionInput = true;
        }
        _inputActions.Enable();
    }

    private void OnDisable() => _inputActions.Disable();
    private void HandleInput()
    {
        MoveInput();
        JumpInput();
        GrabInput();
        SwapInput();
    }
    private void MoveInput()
    {
        _horizontal = _movementInput.x;
        _vertical = _movementInput.y;
        
        _moveAmount = Mathf.Clamp01(Mathf.Abs(_horizontal) + Mathf.Abs(_vertical));
    }

    private void JumpInput()
    {
        if (_jumpInput) _player.Jump();
    }

    private void GrabInput()
    {
        if (_grabInput)
        {
            _player.GrabObject();
            _player.PressButton();
        }
    }

    private void SwapInput()
    {
        if (_swapLeftInput) _player.SwapPrevius();
        else if (_swapRightInput)  _player.SwapNext();
    }
}
