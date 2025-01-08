using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public int health, coins;
    public void TakeDamage(int damage, Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * 5, ForceMode2D.Impulse);
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
        StartCoroutine(ResetColor());
    }
    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
