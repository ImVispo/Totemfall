using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class LavaBurst : Projectile
{
    public override void FireAbility(Enemy enemy)
    {
        SetVelocity((enemy.transform.position - transform.position).normalized);
    }

    public override void DealDamage(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        int _damageDealt = CalculateDamage();
        enemy.TakeDamage(_damageDealt);
        createDamagePopup(_damageDealt, enemy.transform.position);
        Destroy(gameObject);
    }
}
