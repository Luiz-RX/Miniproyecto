using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private CapsuleCollider collider;
    private Health health;
    private Gun gun;
    [SerializeField] private int pickUpType; //0 = heal  1 = energy

    private void Start()
    {
        collider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickUpType == 0) 
            {
                //Dar vida
                health = other.GetComponent<Health>();
                health.GiveHealth(50);
            }
            else if (pickUpType == 1)
            {
                //Dar energia
                gun = other.GetComponentInChildren<Gun>();
                gun.GiveAmmo(100);
            }

            StartCoroutine(ReactivatePickUp());
        }
    }

    private IEnumerator ReactivatePickUp()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false); 
        }

        yield return new WaitForSeconds(30f); 

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true); 
        }
    }
}
