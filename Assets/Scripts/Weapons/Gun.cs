using UnityEngine;

public class Gun : Weapon
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private LayerMask aimColliderLayerMask; // Capas que afectan la mira

    private float nextFireTime = 0f;

    private void Update()
    {
        HandleAiming();
    }

    public override void Shoot()
    {
        if (Time.time >= nextFireTime && ammo > 0)
        {
            Vector3 targetPoint = GetAimPoint(); // Obtenemos la dirección de disparo
            Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));

            ammo--;
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void HandleAiming()
    {
        bool isAiming = Input.GetMouseButton(1); // Click derecho para apuntar

        Vector3 aimTarget = GetAimPoint();
        aimTarget.y = transform.position.y; // Mantiene la altura del jugador
        Vector3 aimDirection = (aimTarget - transform.position).normalized;

        //if (isAiming)
        //{
        //    transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        //}
    }

    private Vector3 GetAimPoint()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, aimColliderLayerMask))
        {
            return hit.point; // Retorna el punto donde impacta el raycast
        }

        return ray.GetPoint(1000f); // Si no impacta, dispara lejos en esa dirección
    }

    public int GetAmmo()
    {
        return ammo;
    }
    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
}
