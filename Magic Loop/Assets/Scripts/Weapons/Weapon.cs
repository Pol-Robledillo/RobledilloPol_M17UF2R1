using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : ScriptableObject
{
    public bool isUnlocked;
    public GameObject projectile;
    public float cooldown;
    public int damage;

    public virtual void Shoot(Vector2 mousePos, PlayerInputManager characterPos) { }
}