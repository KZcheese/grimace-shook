using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerControls;

    [SerializeField] private CharacterController controller;

    [SerializeField] private Vector3 playerVelocity;

    [SerializeField] private bool groundedPlayer;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }

    private void Update()
    {
        groundedPlayer = controller.isGrounded;

        Vector2 movement = _playerControls.Player.Movement.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * (Time.deltaTime * playerSpeed));

        if(move != Vector3.zero)
            gameObject.transform.forward = move;

        // // Changes the height position of the player..
        // if(inputManager.Jumped() && _groundedPlayer)
        //     _playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);


        if(groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(controller.isGrounded && context.performed)
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
}