using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public Transform aimTarget; 
    public float bulletSpeed = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction = (aimTarget.position - firePoint.position).normalized;
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}
