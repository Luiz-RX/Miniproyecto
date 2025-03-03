using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder; // Dónde se equipan las armas
    private Weapon currentWeapon;

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject); // Elimina el arma anterior
        }

        GameObject newWeapon = Instantiate(weaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        currentWeapon = newWeapon.GetComponent<Weapon>();
    }

    void Update()
    {
        if (currentWeapon != null && Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            currentWeapon.Shoot();
        }
    }
}
