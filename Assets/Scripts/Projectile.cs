using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : Ability
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] protected float _projectileSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity * _projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Enemy")
        {
            return;
        }

        DealDamage(collider);
    }

    public abstract void DealDamage(Collider2D collider);

}
