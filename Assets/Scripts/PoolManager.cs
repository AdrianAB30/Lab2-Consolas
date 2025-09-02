using UnityEngine;
using System;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size = 10;
    }

    public static PoolManager Instance;

    public List<Pool> pools;

    public static event Action<string> OnBulletSpawned;

    private Dictionary<string, List<GameObject>> poolDictionary;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        for (int p = 0; p < pools.Count; p++)
        {
            Pool pool = pools[p];
            List<GameObject> list = new List<GameObject>(pool.size);

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);

                var bulletComp = obj.GetComponent<Bullet>();
                if (bulletComp != null)
                {
                    bulletComp.poolTag = pool.tag;
                }

                obj.SetActive(false);
                list.Add(obj);
            }

            poolDictionary[pool.tag] = list;
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary == null) return null;

        if (!poolDictionary.TryGetValue(tag, out var list))
        {
            Debug.LogWarning("Pool con tag " + tag + " no existe.");
            return null;
        }

        for (int i = 0; i < list.Count; i++)
        {
            GameObject obj = list[i];
            if (!obj.activeInHierarchy)
            {
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);

                OnBulletSpawned?.Invoke(tag); 
                return obj;
            }
        }
        return null;
    }

    public int GetAvailable(string tag)
    {
        if (poolDictionary == null)
        {
            var fallback = pools.Find(x => x.tag == tag);
            return fallback != null ? fallback.size : 0;
        }

        if (!poolDictionary.TryGetValue(tag, out var list)) return 0;

        int count = 0;
        for (int i = 0; i < list.Count; i++)
            if (!list[i].activeInHierarchy) count++;

        return count;
    }

    public int GetCapacity(string tag)
    {
        if (poolDictionary == null)
        {
            var fallback = pools.Find(x => x.tag == tag);
            return fallback != null ? fallback.size : 0;
        }

        if (!poolDictionary.TryGetValue(tag, out var list)) return 0;
        return list.Count;
    }
}
