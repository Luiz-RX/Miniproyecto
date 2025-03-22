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

        // Aplicar rotación solo a "heavy gun rotated"
        if (weaponPrefab.name == "ArmaTocha 1")
        {
            weaponHolder.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
            //newWeapon.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        }
        else if(weaponPrefab.name == "Arma Pistola")
        {
            weaponHolder.transform.localRotation = Quaternion.Euler(0f, -90f, 90f);
        }
    }

    void Update()
    {
        if (currentWeapon != null && Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButton(1)) currentWeapon.Shoot();
        }
    }
}
