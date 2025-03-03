using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public float fireRate;
    public int ammo;
    public int maxAmmo;

    public abstract void Shoot();
}