using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public readonly static int IdDie = Animator.StringToHash("Die");
    public Image uiHealth;

    private Animator playerAnimator;

    private PlayerMovement playerMovement;
    private PlayerShooter playerInput;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        playerMovement.enabled = true;
        playerInput.enabled = true;

        uiHealth.gameObject.SetActive(true);
        uiHealth.fillAmount = Health / startingHealth;
    }


    public override void OnDamage(float damage, Vector3 hitpoint, Vector3 hitNormal)
    {

        base.OnDamage(damage, hitpoint, hitNormal);
        uiHealth.fillAmount = Health / startingHealth;
    }

    public override void Die()
    {
        base.Die();

        playerAnimator.SetTrigger(IdDie);

        playerMovement.enabled = false;
        playerInput.enabled = false;

        uiHealth.gameObject.SetActive(false);

    }

    public override void AddHealth(int add)
    {
        base.AddHealth(add);
        uiHealth.fillAmount = Health / startingHealth;
    }
}

