using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootingTotem : Totem
{

    [SerializeField] private float _slowBonus;
    [SerializeField] private float _rootInterval;

    private bool _isRunning;

    private void OnEnable()
    {
        EnemyEnteredEvent += HandleEnemyEntered;
    }

    private void OnDisable()
    {
        EnemyEnteredEvent -= HandleEnemyEntered;
    }

    private void HandleEnemyEntered()
    {
        if (_isRunning)
            return;

        StartCoroutine(RootTimer());
        _isRunning = true;
    }

    private IEnumerator RootTimer()
    {
        // root all enemies
        foreach (Enemy enemy in enemies)
            enemy.Speed = 0;

        yield return new WaitForSeconds(_rootInterval);

        // reset all enemy move speed
        foreach (Enemy enemy in enemies)
            enemy.ResetMoveSpeed();

        yield return new WaitForSeconds(_rootInterval);

        StartCoroutine(RootTimer());
    }

    //protected override void UnitEntered(GameObject gameObject)
    //{
    //    if (gameObject.tag == "Enemy")
    //    {
    //        // grab
    //        StartCoroutine(Root(gameObject.GetComponent<Enemy>()));
    //    }
    //}

    //protected override void UnitExit(GameObject gameObject)
    //{
    //    if (gameObject.tag == "Enemy")
    //    {
    //        updateSlowBonus(gameObject.GetComponent<Enemy>(), _slowBonus);
    //    }
    //}

    //private void updateSlowBonus(Enemy enemy, float slowBonus)
    //{
    //    enemy.Speed += slowBonus;
    //}

    //private IEnumerator Root(Enemy enemy)
    //{
    //    float enemySpeed = enemy.Speed;
    //    updateSlowBonus(enemy, -enemySpeed);
    //    yield return new WaitForSeconds(2);
    //    updateSlowBonus(enemy, enemySpeed - _slowBonus);
    //}
}
