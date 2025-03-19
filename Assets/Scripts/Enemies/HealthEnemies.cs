using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class HealthEnemies : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private CapsuleCollider capsuleCollider;
    private UIManager uiManager;

    [SerializeField] private float destroyTimer = 15f;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        uiManager = FindAnyObjectByType<UIManager>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // Evita que siga recibiendo daño después de morir

        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }

    private void Die()
    {
        isDead = true;
        capsuleCollider.enabled = false;
        navMeshAgent.isStopped = true; // Detiene el movimiento

        int randomValue = Random.Range(0, 2);
        animator.SetTrigger(randomValue == 0 ? "Die1" : "Die2");
        if (uiManager != null) uiManager = FindAnyObjectByType<UIManager>();
        uiManager.EnemyKill();

        StartCoroutine(DestroyAfterTime()); // Inicia el temporizador de eliminación
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f); // Attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 15f); // Detection (Start Chasing)

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 18f); // Stop Chasing
    }
}