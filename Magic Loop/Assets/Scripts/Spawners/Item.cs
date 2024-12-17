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
}
