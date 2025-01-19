using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Flamethrower.asset", menuName = "Weapons/Flamethrower")]

public class Flamethrower : Weapon
{
    private FlamethrowerProjectile spawnedProjectile;
    public bool flamethrowerActive = false;
    public override void Shoot(Vector2 mousePos, PlayerInputManager character)
    {
        if (spawnedProjectile == null)
        {
            Vector2 characterPos = new Vector2(character.transform.position.x, character.transform.position.y);
            Vector2 direction = mousePos - characterPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            spawnedProjectile = Instantiate(projectile, characterPos, rotation).GetComponent<FlamethrowerProjectile>();
            spawnedProjectile.shooter = this;
            spawnedProjectile.playerInputManager = character;
        }
        if (!flamethrowerActive)
        {
            spawnedProjectile.gameObject.SetActive(true);
            spawnedProjectile.hitBox.enabled = true;
            flamethrowerActive = true;
        }
        else
        {
            spawnedProjectile.gameObject.SetActive(false);
            spawnedProjectile.hitBox.enabled = false;
            flamethrowerActive = false;
        }
    }
}
