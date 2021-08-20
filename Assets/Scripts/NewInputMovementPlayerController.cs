using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputMovementPlayerController : MonoBehaviour
{
    private const float playerSpeed = 2.0f;
    private const float gravityValue = -9.81f;
    private const float jumpHeight = 1.0f;
    private CharacterController controller;
    public bool groundedPlayer;
    private Vector3 move;
    public Vector3 playerVelocity;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;

        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero) gameObject.transform.forward = move;

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue inputValue)
    {
        var value = inputValue.Get<Vector2>();
        move = new Vector3(value.x, 0, value.y);
    }

    public void OnJump(InputValue inputValue)
    {
        if (!groundedPlayer || !inputValue.isPressed) return;
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        controller.Move(playerVelocity * Time.deltaTime);
    }
}