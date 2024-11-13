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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = new Inputs();
        inputs.Character.SetCallbacks(this);
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
        if (context.performed)
        {
            direction = context.ReadValue<Vector2>();
            Debug.Log(direction);
        }
        else
        {
            direction = Vector2.zero;
        }
        Move();
    }

    private void Move()
    {
        rb.velocity = direction * speed;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
