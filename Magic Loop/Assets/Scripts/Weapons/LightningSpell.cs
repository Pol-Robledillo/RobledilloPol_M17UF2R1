using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LightningSpell.asset", menuName = "Weapons/LightningSpell")]

public class LightningSpell : Weapon
{
    private Stack<GameObject> projectiles = new Stack<GameObject>();
    public override void Shoot(Vector2 mousePos, Vector2 characterPos)
    {
        if (projectiles.Count > 0)
        {
            Pop(mousePos);
        }
        else
        {
            GameObject bullet = Instantiate(projectile, mousePos, new Quaternion(0, 0, 0, 0));
            bullet.GetComponent<LightningProjectile>().shooter = this;
        }
    }
    public void Push(GameObject projectile)
    {
        projectile.SetActive(false);
        projectiles.Push(projectile);
    }
    public GameObject Pop(Vector2 position)
    {
        GameObject projectile = projectiles.Pop();
        projectile.transform.position = position;
        projectile.SetActive(true);
        return projectile;
    }
}
