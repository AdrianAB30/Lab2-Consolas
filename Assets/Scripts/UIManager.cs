using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text bulletTxt;
    [SerializeField] private string bulletTag = "Bullet"; 

    private void OnEnable()
    {
        PoolManager.OnBulletSpawned += HandlePoolChanged;
        Bullet.OnBulletReturned += HandlePoolChanged;
    }

    private void OnDisable()
    {
        PoolManager.OnBulletSpawned -= HandlePoolChanged;
        Bullet.OnBulletReturned -= HandlePoolChanged;
    }

    void Start()
    {
        RefreshUI();
    }

    private void HandlePoolChanged(string tag)
    {
        if (tag != bulletTag) return;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (PoolManager.Instance == null) return;

        int capacity = PoolManager.Instance.GetCapacity(bulletTag);
        int available = PoolManager.Instance.GetAvailable(bulletTag);

        if (available < 0) available = 0;
        if (available > capacity) available = capacity;

        bulletTxt.text = available + " / " + capacity;
    }
}
