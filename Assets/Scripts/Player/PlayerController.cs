using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D playerRb;
    [SerializeField] private NetworkAnimator playerAnimator;

    [HideInInspector] public Vector2 movement;
    [HideInInspector] public Vector2 lastDirection = Vector2.up;
    [SerializeField] private InputActionProperty moveInputSource;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private static Joystick joystick;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        joystick = DeviceBasedUI.joystick;

    }

    private void Update()
    {
        if (!isOwned)
            return;

        HandleInputs();
        HandleAnimations();
    }

    private void FixedUpdate()
    {
        if (isOwned)
            MovePlayer();
    }

    private void HandleInputs()
    {
        movement = DeviceBasedUI.isMobile ? new Vector2(joystick.Horizontal, joystick.Vertical) : moveInputSource.action.ReadValue<Vector2>();
        spriteRenderer.flipX = movement.x < 0;
    }


    private void HandleAnimations()
    {
        if (movement == Vector2.zero)
        {
            playerAnimator.ResetTrigger("Walk");
            playerAnimator.SetTrigger("Idle");
            return;
        }
        else
        {
            lastDirection = movement.normalized;
            playerAnimator.ResetTrigger("Idle");
            playerAnimator.SetTrigger("Walk");
        }
    }


    private void MovePlayer()
    {
        playerRb.MovePosition(playerRb.position + movement.normalized * moveSpeed * Time.deltaTime);
    }
}
