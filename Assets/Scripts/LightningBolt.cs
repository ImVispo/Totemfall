using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    [SerializeField] private int _damage;
    private int _damageDealt;
    [SerializeField] private Transform pfDamagePopup;
    [SerializeField] private float _overloadChance;
    private bool isCrit = false;
    private List<Enemy> enemies = new List<Enemy>();

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag != "Enemy")
        {
            return;
        }

        Debug.Log(enemies);

        // get enemy component and do some damage to enemy component
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemies.Contains(enemy)) return;
        enemies.Add(enemy);

        _damageDealt = _damage;
        float randValue = Random.value;
        if (randValue < _overloadChance)
        {
            isCrit = true;
            _damageDealt += _damage;
            //lightningBolt.arcCount = 3;
        }

        enemy.DoDamage(_damageDealt);
        createDamagePopup(_damageDealt, enemy.GetComponent<Rigidbody2D>().position, isCrit);
        StartCoroutine(DestroyGameObject());
    }

    private void createDamagePopup(int damage, Vector2 position, bool isCrit = false)
    {
        Transform damagePopupTransform = Instantiate(pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damage, isCrit);
    }

    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
