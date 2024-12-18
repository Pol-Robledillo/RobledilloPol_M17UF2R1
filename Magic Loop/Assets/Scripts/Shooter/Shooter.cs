using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Stack<GameObject> stack;
    void Start()
    {
        stack = new Stack<GameObject>();
    }
    public void Shoot(GameObject projectile, Vector2 direction, float speed)
    {
        if (stack.Count != 0)
        {
            Pop();
        }
        else
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Projectile>().shooter = this;
            bullet.GetComponent<Projectile>().direction = direction;
            bullet.GetComponent<Projectile>().speed = speed;
        }
    }
    private GameObject Pop()
    {
        GameObject obj = stack.Pop();
        obj.SetActive(true);
        obj.transform.position = transform.position;
        return obj;
    }
    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        stack.Push(obj);
    }
}
