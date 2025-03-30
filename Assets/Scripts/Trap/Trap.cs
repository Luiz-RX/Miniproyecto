using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private List<Transform> firePoints; // Lista de puntos de disparo
    [SerializeField] private float fireRate = 1f; // Frecuencia de disparo
    private float nextFireTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Fire()
    {
        foreach (Transform firePoint in firePoints)
        {
            Instantiate(bulletPref, firePoint.position, firePoint.rotation);
        }
    }
}
