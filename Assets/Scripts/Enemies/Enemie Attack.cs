using UnityEngine;

public class EnemieAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 5;
    private Health playerHealth;
    [SerializeField] GameObject attackCollider;


    private void Start()
    { 
        attackCollider.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        playerHealth = FindAnyObjectByType<Health>();
        DoDamage();
    }
    public void DoDamage()
    {
        playerHealth.TakeDamage(attackDamage);
        attackCollider.SetActive(false);
    }

    public void ActivateCollider()
    {
        attackCollider.SetActive(true);
    }
}
