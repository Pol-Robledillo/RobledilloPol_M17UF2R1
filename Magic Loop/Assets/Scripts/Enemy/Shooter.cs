using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shooter : Enemy
{
    public GameObject projectile;
    public float attackSpeed = 1.5f, projectileSpeed = 5f;
    private Stack<GameObject> stack;
    public bool canShoot = true;

    private void Start()
    {
        stack = new Stack<GameObject>();
    }
    public override void Chase() { }
    public override void Idle()
    {
        canShoot = true;
        StopCoroutine(Shoot());
    }
    public override void Attack()
    {
        if (canShoot)
        {
            StartCoroutine(Shoot());
        }
    }
    private IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(attackSpeed);
        anim.SetTrigger("Throw");
        Vector2 playerDirection = player.transform.position - transform.position;
        playerDirection.Normalize();

        if (stack.Count > 0)
        {
            Pop(playerDirection);
        }
        else
        {
            GameObject bullet = Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
            bullet.GetComponent<EnemyProjectile>().shooter = this;
            bullet.GetComponent<EnemyProjectile>().direction = playerDirection;
            bullet.GetComponent<EnemyProjectile>().speed = projectileSpeed;
        }
        yield return StartCoroutine(Shoot());
    }
    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        stack.Push(obj);
    }
    public GameObject Pop(Vector2 direction)
    {
        GameObject obj = stack.Pop();
        obj.transform.position = transform.position;
        obj.GetComponent<EnemyProjectile>().direction = direction;
        obj.SetActive(true);
        return obj;
    }
}
