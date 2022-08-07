using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform player;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float _spawnRate;
    private float _spawnTimer;

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            // spawn
            SpawnEnemy();
            _spawnTimer = _spawnRate;
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate<Enemy>(enemyPrefab, transform.position, Quaternion.identity);
        enemy.SetPlayer(player);
    }
}
