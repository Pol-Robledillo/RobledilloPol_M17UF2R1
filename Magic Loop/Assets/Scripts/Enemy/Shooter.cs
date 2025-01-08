using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shooter : Enemy
{
    public GameObject projectile;
    public float attackSpeed = 1.5f, projectileSpeed = 5f;
    private Stack<GameObject> stack;

    private void Start()
    {
        stack = new Stack<GameObject>();
    }
    public override void Chase() { }
    public override void Idle()
    {
        StopCoroutine(Shoot());
    }
    public override void Attack()
    {
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        Vector2 playerDirection = player.transform.position - transform.position;
        playerDirection.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg);

        if (stack.Count > 0)
        {
            Pop(playerDirection, rotation);
        }
        else
        {
            GameObject bullet = Instantiate(projectile, transform.position, rotation);
            bullet.GetComponent<EnemyProjectile>().shooter = this;
            bullet.GetComponent<EnemyProjectile>().direction = playerDirection;
            bullet.GetComponent<EnemyProjectile>().speed = projectileSpeed;
        }
        yield return new WaitForSeconds(attackSpeed);
        StartCoroutine(Shoot());
    }
    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        stack.Push(obj);
    }
    public GameObject Pop(Vector2 direction, Quaternion rotation)
    {
        GameObject obj = stack.Pop();
        obj.transform.position = transform.position;
        obj.transform.rotation = rotation;
        obj.GetComponent<Projectile>().direction = direction;
        obj.SetActive(true);
        return obj;
    }
}
