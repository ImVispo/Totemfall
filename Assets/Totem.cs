using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Totem : MonoBehaviour
{
    // Gameobject within the radius
    [SerializeField] protected List<Enemy> enemies;

    protected Action EnemyEnteredEvent;

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

    // Keep track of all gameobject inside its radius
}
