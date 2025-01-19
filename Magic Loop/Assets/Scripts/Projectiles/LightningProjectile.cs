using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : MonoBehaviour
{
    private Animator anim;
    private CircleCollider2D hitBox;
    public LightningSpell shooter;
    private void Awake()
    {
        hitBox = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.SetTrigger("Strike");
    }
    public void EnableHitbox()
    {
        hitBox.enabled = true;
    }
    public void BackToShooter()
    {
        hitBox.enabled = false;
        shooter.Push(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 knockBack = collision.gameObject.transform.position - transform.position;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(shooter.damage, knockBack);
        }
    }
}
