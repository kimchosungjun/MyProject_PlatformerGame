using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instace;

    Dictionary<string, GameObject> prefabObjectDic = new Dictionary<string, GameObject>(); // 프리팹 저장
    Dictionary<string, List<Poolable>> poolListDic = new Dictionary<string, List<Poolable>>(); // 생성된 오브젝트 저장

    public EnemySpawnTrigger SpawnTrigger { get; set; } = null;

    int enemyCnt = 0;
    public int EnemyCnt { get { return enemyCnt; } 
        set { enemyCnt = value; GameManager.Instance.UI_Controller.Enemy.UpdateEnemyCnt(enemyCnt);  if (enemyCnt == 0 && SpawnTrigger != null) SpawnTrigger.ActivePortal(); } }

    public GameObject GetObjectPool(string _name)
    {
        if (prefabObjectDic.ContainsKey(_name))
        {
            List<Poolable> poolList = poolListDic[_name];
            int poolCnt = poolList.Count;
            for(int idx =0; idx<poolCnt; idx++)
            {
                if (!poolList[idx].gameObject.activeSelf)
                {
                    poolList[idx].gameObject.SetActive(true);
                    return poolList[idx].gameObject;
                }
            }
            GameObject prefab = prefabObjectDic[_name];
            GameObject go = Instantiate(prefab, this.transform);
            Poolable pool = go.GetComponent<Poolable>();
            if (pool == null)
            {
                go.AddComponent<Poolable>();
                pool = go.GetComponent<Poolable>();
            }
            poolList.Add(pool);
            return go;
        }
        else
        {
            string root = "Prefab/Pool/" + _name;
            GameObject prefab = Resources.Load<GameObject>(root);
            if (prefab == null)
                return null;
            prefabObjectDic.Add(_name, prefab);
            GameObject go = Instantiate(prefab, this.transform);
            Poolable pool = go.GetComponent<Poolable>();
            if (pool == null)
            {
                go.AddComponent<Poolable>();
                pool = go.GetComponent<Poolable>();
            }
            List<Poolable> poolList = new List<Poolable>();
            poolList.Add(pool);
            poolListDic.Add(_name, poolList);
            return go;
        }
    }

    public void EnemySpawner(string _name, Vector2 pos)
    {
        GameObject go = GetObjectPool(_name);
        go.transform.position = pos;
        EnemyCnt += 1;
    }

    public GameObject BossEnemySpawn(string _name, Vector2 pos)
    {
        GameObject go = GetObjectPool(_name);
        go.transform.position = pos;
        EnemyCnt += 1;
        return go;
    }

    public void DeleteBossEnemyList() { EnemyCnt = 0; }
    private void Awake()
    {
        if (Instace == null)
            Instace = this;
        else
            Destroy(this.gameObject);
    }
}
