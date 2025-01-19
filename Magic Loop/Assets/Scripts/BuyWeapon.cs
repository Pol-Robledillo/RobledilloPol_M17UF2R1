using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
{
    public int weaponCost;
    public Weapon weapon;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
            if (playerManager.coins >= weaponCost)
            {
                playerManager.AddCoins(-weaponCost);
                PlayerInputManager player = collision.gameObject.GetComponent<PlayerInputManager>();

                foreach (Weapon w in player.weapons)
                {
                    if (w == weapon)
                    {
                        w.isUnlocked = true;
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
