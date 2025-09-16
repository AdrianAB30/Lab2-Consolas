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
        public bool isBullet;
    }

    public static PoolManager Instance;

    public static event Action<string> OnPoolReloaded;
    public static event Action<string> OnBulletSpawned;
    public static event Action<string> OnPowerUpSpawned; // <- NUEVO evento para powerups

    public List<Pool> pools;

    private Dictionary<string, List<GameObject>> poolDictionary;
    private HashSet<GameObject> usedBullets = new HashSet<GameObject>();

    void OnEnable()
    {
        Bullet.OnBulletReturned += HandleBulletReturned;
    }

    void OnDisable()
    {
        Bullet.OnBulletReturned -= HandleBulletReturned;
    }

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

                if (pool.isBullet)
                {
                    var bulletComp = obj.GetComponent<Bullet>();
                    if (bulletComp != null) bulletComp.poolTag = pool.tag;
                }
                else
                {
                    var powerUpComp = obj.GetComponent<PowerUps>();
                    if (powerUpComp != null) powerUpComp.poolTag = pool.tag;
                }

                obj.SetActive(false);
                list.Add(obj);
            }

            poolDictionary[pool.tag] = list;
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.TryGetValue(tag, out var list)) return null;

        for (int i = 0; i < list.Count; i++)
        {
            GameObject obj = list[i];

            if (!obj.activeInHierarchy)
            {
                position.y += 1f;

                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);

                if (obj.GetComponent<Bullet>() != null)
                {
                    OnBulletSpawned?.Invoke(tag);
                }
                else if (obj.GetComponent<PowerUps>() != null)
                {
                    OnPowerUpSpawned?.Invoke(tag); 
                }

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

    private void HandleBulletReturned(string tag)
    {
        if (!poolDictionary.TryGetValue(tag, out var list)) return;

        for (int i = 0; i < list.Count; i++)
        {
            GameObject obj = list[i];
            if (!obj.activeInHierarchy)
            {
                usedBullets.Add(obj);
                break;
            }
        }
    }

    public void ReloadPool(string tag)
    {
        if (!poolDictionary.TryGetValue(tag, out var list)) return;

        for (int i = 0; i < list.Count; i++)
        {
            GameObject obj = list[i];
            obj.SetActive(false);
        }

        usedBullets.Clear();
        OnPoolReloaded?.Invoke(tag);
    }
}
