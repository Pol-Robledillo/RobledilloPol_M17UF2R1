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
    private void Start()
    {
        anim.SetTrigger("Strike");
        WaitForSeconds wait = new WaitForSeconds(0.3f);
        hitBox.enabled = true;
        WaitForSeconds wait2 = new WaitForSeconds(0.1f);
        shooter.Push(gameObject);
    }
    private void OnEnable()
    {
        anim.SetTrigger("Strike");
        WaitForSeconds wait = new WaitForSeconds(0.3f);
        hitBox.enabled = true;
        WaitForSeconds wait2 = new WaitForSeconds(0.1f);
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
