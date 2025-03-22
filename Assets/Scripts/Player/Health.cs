using UnityEngine;

public class Health : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    [SerializeField] private int health = 100;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            animator.SetTrigger("Die");
            characterController.enabled = false;
            
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }

    public void GiveHealth(int healthToHeal)
    {
        health = Mathf.Clamp((health + healthToHeal), 0, 100);
    }
    public int GetHealth()
    {
        return health;
    }
    }
