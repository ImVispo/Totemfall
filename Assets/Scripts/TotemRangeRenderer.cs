using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemRangeRenderer : MonoBehaviour
{
    // Circle range sprite
    [SerializeField] private Sprite _sprite = default;
    [SerializeField] private Color _color = default;

    [SerializeField] private GameObject _spriteObject;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CircleCollider2D _circleCollider;

    private void Reset()
    {
        _circleCollider = GetComponent<CircleCollider2D>();

        _spriteObject = new GameObject("TotemRangeRenderer");
        _spriteObject.transform.position = transform.position;
        _spriteObject.transform.SetParent(transform);
        _spriteRenderer = _spriteObject.AddComponent<SpriteRenderer>();
    }

    private void OnValidate()
    {
        _spriteObject.transform.localScale = Vector3.one * _circleCollider.radius * 2;
        _spriteRenderer.sprite = _sprite;
        _spriteRenderer.color = _color;
    }

    private void OnDestroy()
    {
        Destroy(_spriteObject);
    }
}
