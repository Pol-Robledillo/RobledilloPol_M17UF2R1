using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed;
    private float waitTime = 0.5f, moveTime = 0.75f;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Movement());
    }
    private IEnumerator Movement()
    {
        rb.velocity = new Vector2(speed, 0);
        yield return new WaitForSeconds(moveTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(waitTime);
        speed *= -1;
        yield return StartCoroutine(Movement());
    }
}
