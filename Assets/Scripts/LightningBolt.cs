using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : Ability
{
    public override void FireAbility(Enemy enemy)
    {
        LightningBolt2D lightningBolt = GetComponent<LightningBolt2D>();
        lightningBolt.startPoint = transform.position;
        lightningBolt.endPoint = enemy.transform.position;

        int _damageDealt = CalculateDamage(); 

        if (isCrit) lightningBolt.arcCount = 3;
        lightningBolt.FireOnce();

        enemy.TakeDamage(_damageDealt);
        createDamagePopup(_damageDealt, enemy.transform.position);
        StartCoroutine(DestroyGameObject());
    }

    private IEnumerator DestroyGameObject()
    {
        Destroy(gameObject.GetComponent<Collider2D>());
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
