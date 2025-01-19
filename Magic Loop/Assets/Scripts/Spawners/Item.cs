using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        HealthPotion,
        Coin,
        DamageBoost,
        SpeedBoost,
        HealthBoost
    }
    public float spawnChance;
    public ItemType itemType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (itemType)
            {
                case ItemType.HealthPotion:
                    collision.gameObject.GetComponent<PlayerManager>().Heal(25);
                    break;
                case ItemType.Coin:
                    collision.gameObject.GetComponent<PlayerManager>().AddCoins(1);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
