using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Balas")]
    [SerializeField] private TMP_Text[] bulletTxt;
    [SerializeField] private string bulletTag = "Bullet";

    [Header("PowerUp")]
    [SerializeField] private TMP_Text powerUpTimerTxt;
    private Coroutine timerCoroutine;

    private void OnEnable()
    {
        PoolManager.OnBulletSpawned += HandlePoolChanged;
        Bullet.OnBulletReturned += HandlePoolChanged;
        PoolManager.OnPoolReloaded += HandlePoolChanged;
        PowerUps.OnPowerUpTimerStarted += StartPowerUpTimer;
    }

    private void OnDisable()
    {
        PoolManager.OnBulletSpawned -= HandlePoolChanged;
        Bullet.OnBulletReturned -= HandlePoolChanged;
        PoolManager.OnPoolReloaded -= HandlePoolChanged;
        PowerUps.OnPowerUpTimerStarted -= StartPowerUpTimer;
    }

    void Start()
    {
        RefreshUI();
        powerUpTimerTxt.text = "";
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

        for (int i = 0; i < bulletTxt.Length; i++)
        {
            bulletTxt[i].text = available + " / " + capacity;
        }
    }
    private void StartPowerUpTimer(float duration)
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        if (duration > 0)
            timerCoroutine = StartCoroutine(TimerRoutine(duration));
        else
            powerUpTimerTxt.text = ""; 
    }
    private IEnumerator TimerRoutine(float duration)
    {
        float timeLeft = duration;

        while (timeLeft > 0)
        {
            powerUpTimerTxt.text = "PowerUp: " + timeLeft.ToString("F1") + "s";
            yield return new WaitForSeconds(0.1f);
            timeLeft -= 0.1f;
        }

        powerUpTimerTxt.text = "";
    }
}
