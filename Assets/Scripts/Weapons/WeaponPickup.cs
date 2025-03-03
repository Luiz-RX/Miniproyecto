using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab; // Prefab del arma a recoger

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponManager weaponManager = other.GetComponent<WeaponManager>();
            if (weaponManager)
            {
                weaponManager.EquipWeapon(weaponPrefab);
                Destroy(gameObject); // Destruir el objeto de recogida
            }
        }
    }
}
