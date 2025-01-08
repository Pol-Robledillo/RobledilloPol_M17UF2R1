using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum States
{
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
    public States currentState;
    public Rigidbody2D rb;
    public Animator anim;

    void Update()
    {
        switch (currentState)
        {
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        DamageAnimation();
        CheckIfAlive();
    }

    public void DamageAnimation()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ResetColor", 0.1f);
    }
}