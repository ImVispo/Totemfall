using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _input;

    [SerializeField] private float _speed;
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    // Project prefab to spawn
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _projectileSpeed;

    // Physics component
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float _shootCooldown;
    private bool _canShoot = true;

    [SerializeField] private int _health;
    [SerializeField] private Slider healthBar;

    [SerializeField] private float totemSpawnRange;
    [SerializeField] private List<Totem> totemPrefabs;
    [SerializeField] private SpriteRenderer rangeIndicator;

    [SerializeField] private GameObject _lightningBolt;

    // Are we currently trying to place a totem
    private bool _isSpawningTotem;
    private GameObject _totemSpawnIndicator;
    private Totem _selectedTotem = null;

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown("1"))
        {
            StartSpawnTotem(totemPrefabs[0]);
        }

        if (Input.GetKeyDown("2"))
        {
            StartSpawnTotem(totemPrefabs[1]);
        }

        if (_isSpawningTotem && _totemSpawnIndicator != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            _totemSpawnIndicator.transform.position = mousePos;
            if (Vector2.Distance(mousePos, transform.position) > totemSpawnRange)
            {
                _totemSpawnIndicator.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, .5f);
            } else
            {
                _totemSpawnIndicator.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
            }
        }

        if (Input.GetKey("mouse 0"))
        {
            ShootLightningBolt();
        }

        if (Input.GetKeyDown("mouse 1"))
        {
            SpawnTotem();
        }

        _input = new Vector2(
        Input.GetAxisRaw("Horizontal"),
        Input.GetAxisRaw("Vertical")
        ).normalized;

        if (_input.x > 0)
        {
            Transform orcTransform = transform.Find("Orc");
            Vector3 scale = orcTransform.localScale;
            scale.x = Mathf.Abs(scale.x);
            orcTransform.localScale = scale;
        }
        else if (_input.x < 0)
        {
            Transform orcTransform = transform.Find("Orc");
            Vector3 scale = orcTransform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            orcTransform.localScale = scale;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = _input * _speed;
    }

    // Returns normalized vector of direction towards mouse
    protected Vector2 GetAimDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition - (Vector2)transform.position).normalized;
    }

    private void ShootProjectile()
    {
        if (!_canShoot || _isSpawningTotem)
            return;

        StartCoroutine(ShootCooldownTimer());
        GameObject instantiatedProjectile = Instantiate(_projectile, transform.position, Quaternion.identity);
        Projectile projectile = instantiatedProjectile.GetComponent<Projectile>();
        projectile.SetVelocity(GetAimDirection() * _projectileSpeed);
    }

    private void ShootLightningBolt()
    {
        if (!_canShoot || _isSpawningTotem)
            return;

        StartCoroutine(ShootCooldownTimer());
        GameObject gameObject = Instantiate<GameObject>(_lightningBolt, transform.position, Quaternion.identity);
        LightningBolt2D lightningBolt = gameObject.GetComponent<LightningBolt2D>();
        lightningBolt.startPoint = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lightningBolt.endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lightningBolt.FireOnce();
    }

    private IEnumerator ShootCooldownTimer()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_shootCooldown);
        _canShoot = true;
    }

    public void DoDamage(int damage)
    {
        _health -= damage;
        healthBar.SetSize(_health / 100f);
    }

    private void StartSpawnTotem(Totem totemPrefab)
    {
        if  (_totemSpawnIndicator != null)
            Destroy(_totemSpawnIndicator);

        _selectedTotem = totemPrefab;
        _isSpawningTotem = true;
        rangeIndicator.enabled = true;

        Sprite sprite = totemPrefab.GetComponent<SpriteRenderer>().sprite;
        _totemSpawnIndicator = new GameObject("TotemSpawnIndicator");
        SpriteRenderer sr = _totemSpawnIndicator.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Top";
        sr.sprite = sprite;
        Color color = sr.color;
        color.a /= 2;
    }

    public void SpawnTotem()
    {
        if (_selectedTotem == null)
            return;

        _isSpawningTotem = false;
        rangeIndicator.enabled = false;
        if (_totemSpawnIndicator != null)
            Destroy(_totemSpawnIndicator);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(mousePosition, transform.position) > totemSpawnRange) return;
        Instantiate<Totem>(_selectedTotem, (Vector3)mousePosition, Quaternion.identity);
        _selectedTotem = null;
    }
}
