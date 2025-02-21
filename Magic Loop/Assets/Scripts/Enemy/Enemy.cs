using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum States
{
    None,
    Attack,
    Idle,
    Chase,
    Die
}
public abstract class Enemy : MonoBehaviour, IDamageable
{
    public Canvas healthCanvas;
    public Slider healthbar;
    public GameObject player;
    public int health;
    public int damage;
    public States currentState = States.None;
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        switch (currentState)
        {
            case States.None:
                break;
            case States.Attack:
                Attack();
                break;
            case States.Idle:
                Idle();
                break;
            case States.Chase:
                Chase();
                break;
            case States.Die:
                Die();
                break;
        }
        if (player != null)
        {
            if (player.transform.position.x - transform.position.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }

    public abstract void Attack();
    public abstract void Idle();
    public abstract void Chase();
    public void Die()
    {
        GameManager.instance.enemiesInFloor--;
        if (GameManager.instance.enemiesInFloor <= 0)
        {
            GameManager.instance.EndAvailable();
        }
        Destroy(gameObject);
    }

    public bool CheckIfAlive()
    {
        if (health <= 0)
        {
            currentState = States.Die;
            return false;
        }
        return true;
    }
    public void TakeDamage(int damage, Vector2 direction)
    {
        StartCoroutine(Knockback(direction));
        health -= damage;
        UpdateHealthbar();
        DamageAnimation();
        CheckIfAlive();
    }
    public void StartDOT(int damage)
    {
        StartCoroutine(DOT(damage));
    }
    public void StopDOT()
    {
        StopCoroutine(DOT(0));
    }
    public IEnumerator DOT(int damage)
    {
        TakeDamage(damage, Vector2.zero);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DOT(damage));
    }
    public IEnumerator Knockback(Vector2 direction)
    {
        direction.Normalize();
        rb.AddForce(direction * 7.5f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector2.zero;
    }
    public void UpdateHealthbar()
    {
        healthCanvas.enabled = true;
        healthbar.value = health;
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