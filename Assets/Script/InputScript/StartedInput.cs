using UnityEngine;
using UnityEngine.InputSystem;

public class StartedInput : MonoBehaviour
{
    public Vector2 look;
    public Vector3 move;
    public bool jump;
    public bool shootLeft;
    public bool shootRight; // NUEVO
    public float scroll; // NUEVO

    private PlayerInput playerInput;

    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.actions["Look"].performed += OnLook;
            playerInput.actions["Look"].canceled += OnLookCanceled;
            playerInput.actions["Move"].performed += OnMove;
            playerInput.actions["Move"].canceled += OnMoveCanceled;
            playerInput.actions["Jump"].performed += OnJump;
            playerInput.actions["Jump"].canceled += OnJumpCanceled;
            playerInput.actions["ShootLeft"].performed += OnShootLeft;
            playerInput.actions["ShootRight"].performed += OnShootRight;
            playerInput.actions["Scroll"].performed += OnScroll;
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.actions["Look"].performed -= OnLook;
            playerInput.actions["Look"].canceled -= OnLookCanceled;
            playerInput.actions["Move"].performed -= OnMove;
            playerInput.actions["Move"].canceled -= OnMoveCanceled;
            playerInput.actions["Jump"].performed -= OnJump;
            playerInput.actions["Jump"].canceled -= OnJumpCanceled;
            playerInput.actions["ShootLeft"].performed -= OnShootLeft;
            playerInput.actions["ShootRight"].performed -= OnShootRight;
            playerInput.actions["Scroll"].performed -= OnShootRight;
            playerInput.actions["Scroll"].performed -= OnScroll;
        }
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        LookValue(context.ReadValue<Vector2>());
    }
    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        LookValue(Vector2.zero);
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        MoveValue(context.ReadValue<Vector2>());
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveValue(Vector2.zero);
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        JumpInput(true);
    }
    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        JumpInput(false);
    }
    private void OnShootLeft(InputAction.CallbackContext context)
    {
        ShootInputLeft(true);
    }
    private void OnShootRight(InputAction.CallbackContext context) 
    {
        ShootInputRight(true); 
    }
    private void OnScroll(InputAction.CallbackContext context)
    {
        ScrollValue(context.ReadValue<float>());
    }


    private void LookValue(Vector2 value)
    {
        look = value;
    }
    private void MoveValue(Vector2 value)
    {
        move = new Vector3(value.x, 0, value.y);
    }
    public void JumpInput(bool value)
    {
        jump = value;
    }
    public void ShootInputLeft(bool value)
    {
        shootLeft = value;
    }
    public void ShootInputRight(bool value) 
    {
        shootRight = value; 
    }
    public void ScrollValue(float value)
    {
        scroll = value;
    }
}
