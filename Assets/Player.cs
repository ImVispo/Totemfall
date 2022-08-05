using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _input;

    // Project prefab to spawn
    [SerializeField] private GameObject _projectile;

    [SerializeField] private float _speed;
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    // Physics component
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float _projectileSpeed;

    [SerializeField] private float _shootCooldown;
    private bool _canShoot = true;

    // Returns normalized vector of direction towards mouse
    protected Vector2 GetAimDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition - (Vector2)transform.position).normalized;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey("mouse 0"))
            ShootProjectile();

        _input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        if (_input.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (_input.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    private void ShootProjectile()
    {
        if (!_canShoot)
            return;

        StartCoroutine(ShootCooldownTimer());
        GameObject instantiatedProjectile = Instantiate(_projectile, transform.position, Quaternion.identity);
        Projectile projectile = instantiatedProjectile.GetComponent<Projectile>();
        projectile.SetVelocity(GetAimDirection() * _projectileSpeed);
    }

    private IEnumerator ShootCooldownTimer()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_shootCooldown);
        _canShoot = true;
    }


    private void FixedUpdate()
    {
        rb.velocity = _input * _speed;
    }

}
