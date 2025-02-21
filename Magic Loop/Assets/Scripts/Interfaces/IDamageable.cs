using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage, Vector2 direction);
    void DamageAnimation();
    void UpdateHealthbar();
}
