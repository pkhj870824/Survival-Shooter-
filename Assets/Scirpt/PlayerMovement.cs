using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static readonly int HashMove = Animator.StringToHash("Move");

    public float moveSpeed = 5f;

    private Animator playerAnimator;
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Camera mainCamera;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        playerAnimator.SetFloat(HashMove, playerInput.Move, 0.15f, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // WASD 이동
        Vector3 delta = playerInput.MoveDirection * moveSpeed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + delta);

        // 마우스 방향 회전
        Ray ray = mainCamera.ScreenPointToRay(playerInput.MousePosition);
        Plane plane = new Plane(Vector3.up, playerRigidbody.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 direction = ray.GetPoint(distance) - playerRigidbody.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);

                playerRigidbody.MoveRotation(rotation);
            }
        }
    }
}