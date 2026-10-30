using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public Transform fireTransform;
    public ParticleSystem muzzleFlashEffect;
    public GunData gunData;
    public LayerMask targetLayer;

    private LineRenderer bulletLineRenderer;

    private float fireDistance = 50f;
    private float lastFireTime;

    private void Awake()
    {
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void OnEnable()
    {
        lastFireTime = 0f;
    }

    public void Fire()
    {
        if (Time.time >= lastFireTime + gunData.timeBetFire)
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 hitPosition;

        Ray ray = new Ray(fireTransform.position, fireTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, fireDistance, targetLayer))
        {

            hitPosition = hit.point;

            IDamageable target = hit.collider.GetComponentInParent<IDamageable>();

            if (target != null)
            {
                target.OnDamage(gunData.damage, hit.point, hit.normal);
            }
        }
        else
        {

            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        StartCoroutine(CoShotEffect(hitPosition));
    }

    private IEnumerator CoShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);

        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false;
    }
}