using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int health = 100;
    void Update()
    {
        //Mostrar en pantalla la vida
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            //Morir
        }
        else
        {
            //Recibir daño animacion
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
