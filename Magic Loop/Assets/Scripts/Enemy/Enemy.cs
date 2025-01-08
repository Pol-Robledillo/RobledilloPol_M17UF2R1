using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public GameObject player;
    public int health;
    public int damage;
    public States currentState = States.None;
    public Rigidbody2D rb;
    public Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
    }

    public abstract void Attack();
    public abstract void Idle();
    public abstract void Chase();
    public void Die()
    {
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
        rb.AddForce(direction * 1, ForceMode2D.Force);
        health -= damage;
        DamageAnimation();
        CheckIfAlive();
    }

    public void DamageAnimation()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(ResetColor());
    }
    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.black;
    }
}