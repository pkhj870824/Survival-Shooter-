using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.Fire)
        {
            gun.Fire();
        }
    }
}