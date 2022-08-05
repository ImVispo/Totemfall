using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
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

        // destroy this projectile
        Destroy(gameObject);
    }
}
