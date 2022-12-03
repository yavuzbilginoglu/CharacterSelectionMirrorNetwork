using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private CharacterController controller = null;
    [SerializeField] public Animator animator = null;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 5f;
    private bool jumppressed = false;
    private float gravityValue = -9.81f;

    

    private Vector2 previousInput;

    public override void OnStartAuthority()
    {
        enabled = true;

        InputManager.Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        InputManager.Controls.Player.Move.canceled += ctx => ResetMovement();    
    }


    [ClientCallback]
    private void Update()
    {
        Move();
        MovementJump();
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    [Client]
    private void SetMovement(Vector2 movement)
    {
        previousInput = movement;
        animator.SetBool("isMoving", true);
    }

    [Client]
    private void ResetMovement()
    {
        previousInput = Vector2.zero;
        animator.SetBool("isMoving", false);
    }

    [Client]
    private void Move()
    {
        Vector3 right = controller.transform.right;
        Vector3 forward = controller.transform.forward;
        right.y = 0f;
        forward.y = 0f;

        Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;

        controller.Move(movement * movementSpeed * Time.deltaTime);
    }

    [Client]
    private void MovementJump()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            playerVelocity.y = 0f;
        }

        if (jumppressed && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * gravityValue * -1);
            jumppressed = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    [Client]
    void OnJump()
    {
        if (controller.velocity.y == 0)
        {
            jumppressed = true;
        }
        else
        {

        }
    }
}