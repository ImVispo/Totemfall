using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWellTotem : Totem
{
    [SerializeField] float _maxHealthDamageModifier;
    [SerializeField] private Transform _pfDamagePopup;

    protected override void OnTotemUpdate()
    {
        foreach (Enemy enemy in enemies) {
            Vector3 direction = transform.position - enemy.transform.position;
            enemy.MoveCharacter(direction);
        }
    }
    protected override void UnitEntered(GameObject gameObject)
    {
        if (gameObject.tag == "Enemy")
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            enemy.CanMove(false);
            enemy.Speed = 5;
        }
    }

    protected override void OnTotemExpired()
    {
        foreach(Enemy enemy in enemies.ToArray())
        {
            enemy.ResetMoveSpeed();
            enemy.CanMove(true);
            float damage = _maxHealthDamageModifier * enemy.GetMaxHealth();
            createDamagePopup((int)damage, enemy.transform.position);
            enemy.TakeDamage((int)damage);
        }
    }

    public virtual void createDamagePopup(int damageDealt, Vector2 position)
    {
        Transform damagePopupTransform = Instantiate(_pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageDealt, false);
    }
}
