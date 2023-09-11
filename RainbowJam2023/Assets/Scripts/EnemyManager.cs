using System;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    List<EnemyController> enemies = new List<EnemyController>();
    public EnemyController enemyPrefab;
    public static EnemyManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        //find all enemies by tag
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemyObjects)
        {
            EnemyController enemy = enemyObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }
    }

    public EnemyController SpawnEnemy()
    {
        EnemyController enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemies.Add(enemy);
        return enemy;
    }
    
    public List<EnemyController> GetEnemies()
    {
        return enemies;
    }


    public void RemoveEnemy(EnemyController enemyController)
    {
        enemies.Remove(enemyController);
    }
}