using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public float speed;
    public Shooter shooter;
    public Vector2 direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
            anim.SetTrigger("BlowUp");
            Vector2 direction = collision.gameObject.transform.position - transform.position;
            direction.Normalize();
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(shooter.damage, direction);
            shooter.Push(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        shooter.Push(gameObject);
    }
    private void Update()
    {
        if (direction.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));
    }
}
