using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);

    }
    private void Update()
    {
        if (transform.localPosition.x <= -16)
        {
            transform.localPosition = new Vector2(17, 0);
        }
    }
}
