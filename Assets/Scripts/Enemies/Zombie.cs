using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private Animator animator;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
        {
            int randomValue = Random.Range(0, 2);

            if (randomValue == 0)
            {
                animator.SetTrigger("Die1");

            }
            else
            {
                animator.SetTrigger("Die2");
            }
            
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f); //Attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 15f); //Detection (Start Chasing)

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 18f); //Stop Chasing
    }
}
