using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private Transform pfDamagePopup;
    [SerializeField] private int _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag != "Enemy")
        {
            return;
        }

        // get enemy component and do some damage to enemy component
        Enemy enemy = collider.GetComponent<Enemy>();
        enemy.DoDamage(_damage);
        createDamagePopup(_damage, false);

        // destroy this projectile
        Destroy(gameObject);
    }

    private void createDamagePopup(int damage, bool isCrit=false)
    {
        Transform damagePopupTransform = Instantiate(pfDamagePopup, _rigidbody.position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damage, isCrit);
    }
}
