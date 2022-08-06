using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Totem : MonoBehaviour
{
    // Gameobject within the radius
    [SerializeField] private List<GameObject> _gameObjects;

    // Components
    void Awake()
    {
    }

    // Other
    void Start()
    {
    }

    // Input
    void Update()
    {
    }

    // Physics
    void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _gameObjects.Add(collider.gameObject);
        UnitEntered(collider.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _gameObjects.Remove(collider.gameObject);
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
