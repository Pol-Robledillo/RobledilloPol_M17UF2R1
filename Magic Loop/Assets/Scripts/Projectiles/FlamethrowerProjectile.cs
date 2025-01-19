using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerProjectile : MonoBehaviour
{
    public ParticleSystem particles;
    public PlayerInputManager playerInputManager;
    public PolygonCollider2D hitBox;
    public Flamethrower shooter;
    public Vector2 direction;
    private void Awake()
    {
        hitBox = GetComponent<PolygonCollider2D>();
        particles = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.StartDOT(shooter.damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.StopDOT();
        }
    }
    private void Update()
    {
        if (playerInputManager != null)
        {
            Vector2 characterPos = new Vector2(playerInputManager.transform.position.x, playerInputManager.transform.position.y);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePos - characterPos;
            direction.Normalize();
            transform.position = characterPos + direction;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
    }
}
