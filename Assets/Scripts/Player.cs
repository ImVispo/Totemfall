using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _input;

    public ParticleSystem dust;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Slider healthBar;

    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    [SerializeField] private float totemSpawnRange;
    [SerializeField] private SpriteRenderer rangeIndicator;
    [SerializeField] private List<Totem> totemPrefabs;

    private bool _isSpawningTotem;
    private GameObject _totemSpawnIndicator;
    private Totem _selectedTotem = null;

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown("1")) StartSpawnTotem(totemPrefabs[0]);
        if (Input.GetKeyDown("2")) StartSpawnTotem(totemPrefabs[1]);
        if (Input.GetKeyDown("3")) StartSpawnTotem(totemPrefabs[2]);

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

        if (Input.GetKeyDown("mouse 0"))
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
        if (rb.velocity.magnitude > 0)
        {
            CreateDust();
        } 
    }

    public void TakeDamage(int damage)
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

    private void CreateDust()
    {
        dust.Play();
    }
}
