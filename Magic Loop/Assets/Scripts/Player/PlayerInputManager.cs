using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour, Inputs.ICharacterActions
{
    private bool canAttack = true;
    public float speed;
    public Weapon[] weapons;
    private Weapon currentWeapon; 
    private Inputs inputs;
    private Vector2 direction;
    private Rigidbody2D rb;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputs = new Inputs();
        inputs.Character.SetCallbacks(this);
    }
    private void Start()
    {
        if (weapons.Length > 0 && weapons != null)
        {
            currentWeapon = weapons[0];
        }
        else
        {
            Debug.Log("No weapons found");
        }
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

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (canAttack)
        {
            Vector2 characterPos = transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Vector2 mouseDirection = mousePos - characterPos;
            mouseDirection.Normalize();
            Debug.Log(mouseDirection);

            anim.SetFloat("x", mouseDirection.x);
            anim.SetFloat("y", mouseDirection.y);
            anim.SetTrigger("Attack");

            currentWeapon.Shoot(mousePos, characterPos);
            StartCoroutine(AttackCooldown(currentWeapon.cooldown));
        }
    }
    private IEnumerator AttackCooldown(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
        StopCoroutine(AttackCooldown(cooldown));
    }
}