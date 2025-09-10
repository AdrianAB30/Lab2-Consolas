using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUp", menuName = "PowerUps/PowerUp", order = 1)]
public class PowerUpData : ScriptableObject
{
    public enum PowerUpType { Ammo, Speed, Freeze }
    public PowerUpType type;

    [Header("Config - Speed")]
    public float speedMultiplier = 1.5f;
    public float speedDuration = 5f;

    [Header("Config - Freeze")]
    public float freezeDuration = 3f;
}