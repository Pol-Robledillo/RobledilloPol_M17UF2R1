using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour, Inputs.ICharacterActions
{
    public float speed;
    private Inputs inputs;
    private Vector2 direction;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputs = new Inputs();
        inputs.Character.SetCallbacks(this);
    }
    private void Update()
    {
        if (direction.x != 0 || direction.y != 0)
        {
            anim.SetFloat("x", direction.x);
            anim.SetFloat("y", direction.y);
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.Character.Movement.performed += OnMovement;
    }

    private void OnDisable()
    {
        inputs.Disable();
        inputs.Character.Movement.performed -= OnMovement;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.velocity = direction * speed;
    }
}
