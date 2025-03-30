using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    [SerializeField] private int bulletDamage;

    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 50f;
        bulletRigidbody.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthEnemies enemyHealth = other.GetComponent<HealthEnemies>();
        Health playerHealth = other.GetComponent<Health>();

        bool hitSomething = false;

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(bulletDamage);
            hitSomething = true;
        }

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(bulletDamage);
            hitSomething = true;
        }

        Instantiate(hitSomething ? vfxHitGreen : vfxHitRed, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f); // Da tiempo para que los efectos se reproduzcan
    }

}
