using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 5;
    private Health playerHealth;
    [SerializeField] private GameObject attackCollider;

    private void Start()
    {
      attackCollider.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log($"El enemigo ha hecho {attackDamage} de daño a {other.name}");
            }
            else
            {
                Debug.LogError($"El objeto {other.name} no tiene un componente Health.");
            }
        }
    }

   

    public void ActivateCollider()
    {
            attackCollider.SetActive(true);
    }
    public void DeactivateCollider()
    {
        attackCollider.SetActive(false);
    }
}
