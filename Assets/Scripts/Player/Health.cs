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
            //Recibir da�o animacion
        }
    }

    public int GetHealth()
    {
        return health;
    }
    }
