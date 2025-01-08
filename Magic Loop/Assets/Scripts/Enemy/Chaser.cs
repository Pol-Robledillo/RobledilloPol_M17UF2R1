using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{
    public float speed;
    private Vector2 startPosition;

    public override void Attack()
    {
        rb.velocity = Vector2.zero;
        GetComponent<CircleCollider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
        }
    }
    public override void Idle()
    {
        currentState = States.Chase;
    }
    public override void Chase()
    {
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckIfAlive())
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                currentState = States.Attack;
            }
        }
    }
}
