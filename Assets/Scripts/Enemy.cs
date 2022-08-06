using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    public Transform player;
    [SerializeField] private Rigidbody2D rb;

    [Space]
    [Header("Settings")]
    [SerializeField] private int _health;
    [SerializeField] private float _baseSpeed;
    private float _speed;
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    private Vector2 movement;

    private void Start()
    {
        _speed = _baseSpeed;
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        movement = direction.normalized;
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * _speed * Time.deltaTime));
    }

    public void ResetMoveSpeed()
    {
        Speed = _baseSpeed;
    }

    public void DoDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }


}