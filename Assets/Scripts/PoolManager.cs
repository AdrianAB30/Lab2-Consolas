using UnityEngine;
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
                obj.SetActive(false);
                list.Add(obj);
            }

            poolDictionary[pool.tag] = list;
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        List<GameObject> list;
        if (!poolDictionary.TryGetValue(tag, out list))
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
                return obj;
            }
        }
        return null;
    }
}
