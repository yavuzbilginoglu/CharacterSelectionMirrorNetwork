using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : NetworkBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight=5f;
    private bool jumppressed=false;
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller= GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovementJump();
    }

    private void MovementJump()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer) 
        {
            playerVelocity.y = 0f;
        }

        if (jumppressed &&groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * gravityValue * -1);
            jumppressed=false;
        }

        playerVelocity.y += gravityValue*Time.deltaTime;
        controller.Move(playerVelocity*Time.deltaTime);
    }
    void OnJump()
    {
        if (controller.velocity.y==0)
        {
            jumppressed=true;
        }
        else
        {

        }
    }
}
