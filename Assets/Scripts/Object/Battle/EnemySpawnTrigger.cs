using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] GameObject portal;

    bool isOnPlayer = false;

    [SerializeField] string[] spawnEnemyNames;
    [SerializeField] int[] spawnEnemyCnt;
    [SerializeField] Vector2[] spawnPositions;

    private void Awake()
    {
        portal.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOnPlayer)
        {
            isOnPlayer = true;
            PoolManager.Instace.SpawnTrigger = this;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        int enemyNameCnt = spawnEnemyNames.Length;
        int spawPosCnt = spawnPositions.Length;
        int randNum;
        for(int q = 0; q<enemyNameCnt; q++)
        {
            for(int p = 0; p<spawnEnemyCnt[q]; p++)
            {
                randNum = Random.Range(0, spawPosCnt);
                PoolManager.Instace.EnemySpawner(spawnEnemyNames[q],spawnPositions[randNum]);
            }
        }
    }

    public void ActivePortal()
    {
        portal.SetActive(true);
        PoolManager.Instace.SpawnTrigger = null;
    }
}
