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
    [SerializeField] private int _maxHealth;
    private int _health;
    [SerializeField] private float _baseSpeed;
    private float _speed;
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    [SerializeField] private int _damage;
    [SerializeField] private int _dealDamageTimer;
    private bool _canDealDamage = true;

    private Vector2 movement;
    private bool _canMove = true;

    private void Start()
    {
        _speed = _baseSpeed;
        _health = _maxHealth;
    }

    private void Update()
    {
        if (!_canMove) return;
        Vector3 direction = player.position - transform.position;
        movement = direction.normalized;
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;
        MoveCharacter(movement);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag != "Player")
        {
            return;
        }


        Player player = collider.gameObject.GetComponent<Player>();
        Speed = 0;
        DealDamage(player);
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        ResetMoveSpeed();
    }
  

    public void MoveCharacter(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * _speed * Time.deltaTime));
    }

    public void SetPlayer(Transform p)
    {
        player = p; 
    }

    public void ResetMoveSpeed()
    {
        Speed = _baseSpeed;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DealDamage(Player player)
    {
        if (!_canDealDamage) return;
        StartCoroutine(DealDamageTimer());
        player.TakeDamage(_damage);
    }

    private IEnumerator DealDamageTimer()
    {
        _canDealDamage = false;
        yield return new WaitForSeconds(_dealDamageTimer);
        _canDealDamage = true;
    }

    public void CanMove(bool canMove)
    {
        _canMove = canMove;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

}