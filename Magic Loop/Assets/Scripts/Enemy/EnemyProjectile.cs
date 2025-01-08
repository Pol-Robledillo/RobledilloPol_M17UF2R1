using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public Shooter shooter;
    public Vector2 direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.velocity = direction * speed;
    }
    private void OnEnable()
    {
        rb.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(shooter.damage);
            shooter.Push(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        shooter.Push(gameObject);
    }
}
