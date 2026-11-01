using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public static readonly int IdDie = Animator.StringToHash("Die");

    public Image uiHealth;

    // 플레이어 사운드
    public AudioClip hitClip;
    public AudioClip deathClip;

    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private PlayerShooter playerInput;
    private AudioSource playerAudioSource;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerShooter>();
        playerAudioSource = GetComponent<AudioSource>();
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
        if (IsDead)
        {
            return;
        }

        base.OnDamage(damage, hitpoint, hitNormal);
        Debug.Log($"현재 HP: {Health} / {startingHealth}, 받은 데미지: {damage}");

        uiHealth.fillAmount = Health / startingHealth;

        if (!IsDead && playerAudioSource != null && hitClip != null)
        {
            playerAudioSource.PlayOneShot(hitClip);
        }

    }

    public override void Die()
    {
        if (IsDead)
        {
            return;
        }

        base.Die();

        // 사망 사운드
        if (playerAudioSource != null && deathClip != null)
        {
            playerAudioSource.PlayOneShot(deathClip);
        }

        playerAnimator.SetTrigger(IdDie);

        playerMovement.enabled = false;
        playerInput.enabled = false;
    }

    public override void AddHealth(int add)
    {
        base.AddHealth(add);
        uiHealth.fillAmount = Health / startingHealth;
    }
}