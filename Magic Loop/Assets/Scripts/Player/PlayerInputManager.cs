using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour, Inputs.ICharacterActions
{
    public Sprite selectedSpell, unselectedSpell;
    public Image[] spellSlots;
    private bool canAttack = true;
    public float speed;
    public Weapon[] weapons;
    private Weapon currentWeapon;
    private Inputs inputs;
    private Vector2 direction;
    private Rigidbody2D rb;
    public Animator anim;
    public int startWeapon = 0;
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
            currentWeapon = weapons[startWeapon];
            weapons[startWeapon].isUnlocked = true;
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
        if (!GameManager.instance.gameOver)
        {
            direction = context.ReadValue<Vector2>();
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void Move()
    {
        rb.velocity = direction * speed;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!GameManager.instance.gameOver)
        {
            if (canAttack)
            {
                Vector2 characterPos = transform.position;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

                Vector2 mouseDirection = mousePos - characterPos;
                mouseDirection.Normalize();

                anim.SetFloat("x", mouseDirection.x);
                anim.SetFloat("y", mouseDirection.y);
                anim.SetTrigger("Attack");

                currentWeapon.Shoot(mousePos, characterPos);
                StartCoroutine(AttackCooldown(currentWeapon.cooldown));
            }
        }
    }
    private IEnumerator AttackCooldown(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
        StopCoroutine(AttackCooldown(cooldown));
    }
    public void OnChangeToWeapon1(InputAction.CallbackContext context)
    {
        ChangeWeapon(0);
    }
    public void OnChangeToWeapon2(InputAction.CallbackContext context)
    {
        ChangeWeapon(1);
    }
    public void OnChangeToWeapon3(InputAction.CallbackContext context)
    {
        ChangeWeapon(2);
    }
    public void OnChangeToWeapon4(InputAction.CallbackContext context)
    {
        ChangeWeapon(3);
    }
    public void ChangeWeapon(int slot)
    {
        if (weapons[slot].isUnlocked)
        {
            currentWeapon = weapons[slot];
            foreach (Image spellSlot in spellSlots)
            {
                spellSlot.sprite = unselectedSpell;
            }
            spellSlots[slot].sprite = selectedSpell;
        }
    }
    public void OnPauseGame(InputAction.CallbackContext context)
    {
        GameManager.instance.TogglePause();
    }
}