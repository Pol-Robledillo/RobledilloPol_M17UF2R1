using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public int health, coins;
    public void TakeDamage(int damage)
    {
        health -= damage;
        DamageAnimation();
        if (health <= 0)
        {
            //GameManager.instance.GameOver();
        }
    }
    public void DamageAnimation()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ResetColor", 0.1f);
    }
}
