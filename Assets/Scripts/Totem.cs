using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Totem : MonoBehaviour
{
    [SerializeField] private float duration;
    private float durationTimer;
    [SerializeField] private Slider _slider;
    private Slider durationSlider;

    // Gameobject within the radius
    [SerializeField] protected List<Enemy> enemies;

    protected Action EnemyEnteredEvent;

    private void Start()
    {
        durationSlider = Instantiate(_slider, transform.position + new Vector3(0, -0.7f, 0), Quaternion.identity);
        if (ColorUtility.TryParseHtmlString("C0CBDC", out Color color))
        {
            durationSlider.spriteRenderer.color = color;
        }
        durationTimer = duration;
    }

    private void Update()
    {
        durationTimer -= Time.deltaTime;
        durationSlider.SetSize(durationTimer / duration);
        if (durationTimer > 0) return;

        Destroy(durationSlider.gameObject);
        OnTotemExpired();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Enemy>(out Enemy e))
            enemies.Add(e);

        UnitEntered(collider.gameObject);

        if (collider.CompareTag("Enemy"))
            EnemyEnteredEvent?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        enemies.Remove(collider.GetComponent<Enemy>());
        UnitExit(collider.gameObject);
    }

    protected virtual void UnitEntered(GameObject gameObject)
    {

    }

    protected virtual void UnitExit(GameObject gameObject)
    {

    }

    protected virtual void OnTotemExpired()
    {

    }

    // Keep track of all gameobject inside its radius
}
