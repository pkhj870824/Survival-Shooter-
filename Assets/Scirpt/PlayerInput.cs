using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string MoveAxis = "Vertical";
    public static readonly string RotateAxis = "Horizontal";
    public static readonly string FireAxis = "Fire1";

    public float Move { get; private set; }
    public float Rotate { get; private set; }
    public bool Fire { get; private set; }


    public Vector3 MoveDirection { get; private set; }
    public Vector2 MousePosition { get; private set; }


    private void OnDisable()
    {
        Move = 0f;
        Rotate = 0f;
        Fire = false;

        MoveDirection = Vector3.zero;
        MousePosition = Vector2.zero;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw(RotateAxis);
        float vertical = Input.GetAxisRaw(MoveAxis);

        MoveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        Move = MoveDirection.magnitude;
        Rotate = horizontal;

        Fire = Input.GetButton(FireAxis);
        MousePosition = Input.mousePosition;
    }
}
