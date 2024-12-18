using UnityEngine;
[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Weapons/Weapon")]

public class Weapon : ScriptableObject
{
    public enum WeaponTypes
    {
        Melee,
        Sniper,
        Thunder,
        Flamethrower
    }
    public GameObject projectile;
    public float cooldown;
    public float damage;
    public float projectileSpeed;
    public WeaponTypes weaponType;
}
