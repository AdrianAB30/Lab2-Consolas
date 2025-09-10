using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData;

    public static event Action<PowerUps> OnEffectFinished;
    private void OnTriggerEnter(Collider other)
    {
        TurretControl turret = other.GetComponentInChildren<TurretControl>();
        TankMovement tank = other.GetComponentInParent<TankMovement>();

        if (turret == null && tank == null) return;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Renderer[] rends = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].enabled = false;
        }

        switch (powerUpData.type)
        {
            case PowerUpData.PowerUpType.Ammo:
                if (turret != null)
                {
                    PoolManager.Instance.ReloadPool(turret.bulletTag);
                    Debug.Log("Munición recargada para " + turret.name);
                }
                OnEffectFinished?.Invoke(this);
                break;

            case PowerUpData.PowerUpType.Speed:
                if (tank != null)
                {
                    StartCoroutine(ApplySpeedBoost(tank, turret));
                    Debug.Log("Velocidad aumentada para " + tank.name + " y " + turret.name);
                }
                break;

            case PowerUpData.PowerUpType.Freeze:
                StartCoroutine(FreezeOtherPlayers(other.gameObject));
                Debug.Log("Otros jugadores congelados");
                break;
        }
    }

    private IEnumerator ApplySpeedBoost(TankMovement tank, TurretControl turret)
    {
        float originalMoveSpeed = tank.moveSpeed;
        float originalRotationSpeed = tank.rotationSpeed;
        float originalH = turret.horizontalRotationSpeed;
        float originalV = turret.verticalRotationSpeed;

        tank.moveSpeed *= powerUpData.speedMultiplier;
        tank.rotationSpeed *= powerUpData.speedMultiplier;
        turret.horizontalRotationSpeed *= powerUpData.speedMultiplier;
        turret.verticalRotationSpeed *= powerUpData.speedMultiplier;

        yield return new WaitForSeconds(powerUpData.speedDuration);

        tank.moveSpeed = originalMoveSpeed;
        tank.rotationSpeed = originalRotationSpeed;
        turret.horizontalRotationSpeed = originalH;
        turret.verticalRotationSpeed = originalV;

        Debug.Log("Velocidad reiniciada para " + tank.name);

        OnEffectFinished?.Invoke(this); 
    }

    private IEnumerator FreezeOtherPlayers(GameObject collector)
    {
        List<TankMovement> allTanks = TankRegistry.Instance.GetAllTanks();

        for (int i = 0; i < allTanks.Count; i++)
            if (allTanks[i].gameObject != collector)
                allTanks[i].enabled = false;

        yield return new WaitForSeconds(powerUpData.freezeDuration);

        for (int i = 0; i < allTanks.Count; i++)
            if (allTanks[i].gameObject != collector)
                allTanks[i].enabled = true;

        OnEffectFinished?.Invoke(this); 
    }
}
