using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public Shooter shooter;
    public Vector2 direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        SetProjectileSpeed();
    }
    private void SetProjectileSpeed()
    {
        rb.velocity = direction * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        shooter.Push(gameObject);
    }
    void Update()
    {
        rb.velocity = direction * speed;
    }
}
