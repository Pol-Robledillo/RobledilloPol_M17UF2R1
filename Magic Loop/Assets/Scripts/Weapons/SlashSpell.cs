using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SlashSpell.asset", menuName = "Weapons/SlashSpell")]

public class SlashSpell : Weapon
{
    private Stack<GameObject> projectiles = new Stack<GameObject>();
    public override void Shoot(Vector2 mousePos, PlayerInputManager character)
    {
        Vector2 characterPos = new Vector2(character.transform.position.x, character.transform.position.y);
        Vector2 direction = mousePos - characterPos;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Vector2 spawnPosition = characterPos + direction;
        if (projectiles.Count > 0)
        {
            Pop(spawnPosition, rotation);
        }
        else
        {
            GameObject bullet = Instantiate(projectile, spawnPosition, rotation);
            bullet.GetComponent<SlashProjectile>().shooter = this;
        }
    }
    public void Push(GameObject projectile)
    {
        projectile.SetActive(false);
        projectiles.Push(projectile);
    }
    public GameObject Pop(Vector2 position, Quaternion rotation)
    {
        GameObject projectile = projectiles.Pop();
        projectile.transform.position = position;
        projectile.transform.rotation = rotation;
        projectile.SetActive(true);
        return projectile;
    }
}
