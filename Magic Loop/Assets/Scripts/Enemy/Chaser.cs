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
        anim.SetTrigger("BlowUp");
    }
    public override void Idle()
    {
        rb.velocity = Vector2.zero;
    }
    public override void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = collision.gameObject.transform.position - transform.position;
            direction.Normalize();
            collision.GetComponent<PlayerManager>().TakeDamage(damage, direction);
            Die();
        }
    }
}
