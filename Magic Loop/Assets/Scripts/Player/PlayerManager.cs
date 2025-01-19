using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public Slider healthbar;
    public TextMeshProUGUI coinText;
    public int health, coins;
    public void TakeDamage(int damage, Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * 5, ForceMode2D.Impulse);
        health -= damage;
        DamageAnimation();
        UpdateHealthbar();
        if (health <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GameManager.instance.gameOver = true;
            GameManager.instance.GameOver();
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
    public void UpdateHealthbar()
    {
        healthbar.value = health;
    }
    public void Heal(int amount)
    {
        health += amount;
        if (health > 100)
        {
            health = 100;
        }
        UpdateHealthbar();
    }
    public void AddCoins(int amount)
    {
        coins += amount;
        coinText.text = coins.ToString();
    }
}
