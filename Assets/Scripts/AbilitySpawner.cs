using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AbilitySpawner : MonoBehaviour
{
    // Gameobject within the radius
    [SerializeField] Ability _abilityPrefab;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private float _fireRate;
    private bool _canFire = true;

    private void Update()
    {
        if (!_canFire || enemies.Count == 0) return;
        StartCoroutine(FireCooldownTimer());

        Enemy closestEnemy = null;
        float closestDistance = 0;
        foreach (Enemy e in enemies)
        {
            float distance = Vector2.Distance(transform.position, e.transform.position);
            if (closestEnemy == null || distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = e;
            }
        }
        if (closestEnemy) {
            Ability ability = Instantiate<Ability>(_abilityPrefab, transform.position, Quaternion.identity);
            ability.FireAbility(closestEnemy);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Enemy>(out Enemy e))
            enemies.Add(e);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        enemies.Remove(collider.GetComponent<Enemy>());
    }

    private IEnumerator FireCooldownTimer()
    {
        _canFire = false;
        yield return new WaitForSeconds(_fireRate);
        _canFire = true;
    }

}
