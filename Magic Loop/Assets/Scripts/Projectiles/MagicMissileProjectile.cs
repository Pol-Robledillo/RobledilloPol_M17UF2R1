using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class MagicMissileProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public MagicMissile shooter;
    public Vector2 direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.velocity = direction * speed;
    }
    private void OnEnable()
    {
        rb.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            direction = collision.gameObject.transform.position - transform.position;
            direction.Normalize();
            collision.gameObject.GetComponent<Enemy>().TakeDamage(shooter.damage, direction);
            shooter.Push(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        shooter.Push(gameObject);
    }
}
