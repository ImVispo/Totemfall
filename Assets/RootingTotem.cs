using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootingTotem : Totem
{

    [SerializeField] private float _slowBonus;

    protected override void UnitEntered(GameObject gameObject)
    {
        if (gameObject.tag == "Enemy")
        {
            // grab
            StartCoroutine(Root(gameObject.GetComponent<Enemy>()));
        }
    }

    protected override void UnitExit(GameObject gameObject)
    {
        if (gameObject.tag == "Enemy")
        {
            updateSlowBonus(gameObject.GetComponent<Enemy>(), _slowBonus);
        }
    }

    private void updateSlowBonus(Enemy enemy, float slowBonus)
    {
        enemy.Speed += slowBonus;
    }

    private IEnumerator Root(Enemy enemy)
    {
        float enemySpeed = enemy.Speed;
        updateSlowBonus(enemy, -enemySpeed);
        yield return new WaitForSeconds(2);
        updateSlowBonus(enemy, enemySpeed - _slowBonus);
    }

}
