using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Transform pfDamagePopup;
    [SerializeField] private float _overloadChance;
    private bool isCrit = false;

    public void dealDamage(Enemy enemy)
    {
        LightningBolt2D lightningBolt = gameObject.GetComponent<LightningBolt2D>();
        lightningBolt.startPoint = transform.position;
        lightningBolt.endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float randValue = Random.value;
        Debug.Log(randValue);
        Debug.Log((1f - _overloadChance));
        if (randValue < _overloadChance)
        {
            isCrit = true;
            _damage += _damage;
            lightningBolt.arcCount = 3;
        }

        lightningBolt.FireOnce();

        enemy.DoDamage(_damage);
        createDamagePopup(_damage, enemy.GetComponent<Rigidbody2D>().position, isCrit);
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
