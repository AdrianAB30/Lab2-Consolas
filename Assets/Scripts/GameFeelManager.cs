using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameFeelManager : MonoBehaviour
{
    public static GameFeelManager Instance;

    [Header("Camera Shake (Rotation)")]
    public List<Camera> mainCameras;
    public float defaultShakeDuration = 0.15f;
    public Vector3 defaultShakeStrength = new Vector3(1f, 1f, 0f); 
    public int defaultShakeVibrato = 15;
    public float randomness = 90f;

    [Header("Gamepad Vibration")]
    public float defaultLowFrequency = 0.25f;  
    public float defaultHighFrequency = 0.5f;  
    public float defaultVibrationDuration = 0.2f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void CameraShake(float duration = -1, Vector3 strength = default, int vibrato = -1, float randomness = -1)
    {
        if (mainCameras == null || mainCameras.Count == 0) return;

        float shakeDuration = duration > 0 ? duration : defaultShakeDuration;
        Vector3 shakeStrength = strength != default ? strength : defaultShakeStrength;
        int shakeVibrato = vibrato > 0 ? vibrato : defaultShakeVibrato;
        float shakeRandomness = randomness > 0 ? randomness : this.randomness;

        for (int i = 0; i < mainCameras.Count; i++)
        {
            mainCameras[i].transform
                .DOShakeRotation(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness, true);
        }
    }
    public void Rumble(float lowFreq = -1, float highFreq = -1, float duration = -1)
    {
        Gamepad pad = Gamepad.current;
        if (pad == null) return;

        float low = lowFreq > 0 ? lowFreq : defaultLowFrequency;
        float high = highFreq > 0 ? highFreq : defaultHighFrequency;
        float dur = duration > 0 ? duration : defaultVibrationDuration;

        pad.SetMotorSpeeds(low, high);
        StartCoroutine(StopRumbleAfter(pad, dur));
    }
    private IEnumerator StopRumbleAfter(Gamepad pad, float dur)
    {
        yield return new WaitForSeconds(dur);
        if (pad != null) pad.SetMotorSpeeds(0, 0);
    }
    public void OnShootFeedback()
    {
        CameraShake();
    }

    public void OnHitFeedback()
    {
        CameraShake(0.25f, new Vector3(2f, 2f, 0f), 20);
    }
}
