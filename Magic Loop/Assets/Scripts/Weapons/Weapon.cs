using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : ScriptableObject
{
    public Image icon;
    public GameObject projectile;
    public float cooldown;
    public int damage;
    public float projectileSpeed;

    public virtual void Shoot(Vector2 mousePos, Vector2 characterPos) { }
}
