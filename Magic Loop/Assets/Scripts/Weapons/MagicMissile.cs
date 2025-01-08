using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MagicMissile.asset", menuName = "Weapons/MagicMissile")]

public class MagicMissile : Weapon
{
    private Stack<GameObject> projectiles = new Stack<GameObject>();
    public override void Shoot(Vector2 mousePos, Vector2 characterPos)
    {
        Vector2 mouseDirection = mousePos - characterPos;
        mouseDirection.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg);

        if (projectiles.Count > 0)
        {
            Pop(characterPos, mouseDirection, rotation, projectileSpeed);
        }
        else
        {
            GameObject bullet = Instantiate(projectile, characterPos, rotation);
            bullet.GetComponent<Projectile>().shooter = this;
            bullet.GetComponent<Projectile>().direction = mouseDirection;
            bullet.GetComponent<Projectile>().speed = projectileSpeed;
        }
    }
    public void Push(GameObject projectile)
    {
        projectile.SetActive(false);
        projectiles.Push(projectile);
    }
    public GameObject Pop(Vector2 position, Vector2 direction, Quaternion rotation, float speed)
    {
        GameObject projectile = projectiles.Pop();
        projectile.transform.position = position;
        projectile.transform.rotation = rotation;
        projectile.GetComponent<Projectile>().speed = speed;
        projectile.GetComponent<Projectile>().direction = direction;
        projectile.SetActive(true);
        return projectile;
    }
}
