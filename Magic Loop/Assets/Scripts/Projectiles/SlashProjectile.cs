using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour
{
    private Animator anim;
    private CapsuleCollider2D hitBox;
    public SlashSpell shooter;
    private void Awake()
    {
        hitBox = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.SetTrigger("Slash");
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
