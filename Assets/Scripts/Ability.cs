using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{

    [SerializeField] protected int _damage;
    [SerializeField] protected float _critChance;
    protected bool isCrit = false;
    [SerializeField] private Transform _pfDamagePopup;

    public virtual void FireAbility(Enemy enemy)
    {


    }

    public virtual int CalculateDamage()
    {
        float randValue = Random.value;
        if (randValue < _critChance)
        {
            isCrit = true;
            return _damage * 2;
        }
        return _damage;
    }

    public virtual void createDamagePopup(int damageDealt, Vector2 position)
    {
        Transform damagePopupTransform = Instantiate(_pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageDealt, isCrit);
    }
}
