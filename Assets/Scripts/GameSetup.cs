using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class GameSetup : MonoBehaviour
{
    [Header("Prefabs en escena")]
    public GameObject tank1;
    public GameObject tank2;

    [Header("Cámaras")]
    public Camera camTank1;
    public Camera camTurret1;
    public Camera camTank2;
    public Camera camTurret2;

    [Header("Inputs")]
    public PlayerInput inputTank1;
    public PlayerInput inputTurret1;
    public PlayerInput inputTank2;
    public PlayerInput inputTurret2;

    private void Start()
    {
        var devices = Gamepad.all.ToArray();

        if (GameManager.playerCount == 2)
        {
            SetupTwoPlayers(devices);
        }
        else if (GameManager.playerCount == 4)
        {
            SetupFourPlayers(devices);
        }
    }
    private void SetupTwoPlayers(Gamepad[] devices)
    {
        tank1.SetActive(true);
        tank2.SetActive(false);

        camTank1.rect = new Rect(0f, 0f, 1f, 0.5f);
        camTurret1.rect = new Rect(0f, 0.5f, 1f, 0.5f);

        camTank2.gameObject.SetActive(false);
        camTurret2.gameObject.SetActive(false);

        inputTank1.enabled = true;
        inputTurret1.enabled = true;
        inputTank2.enabled = false;
        inputTurret2.enabled = false;

        inputTank1.SwitchCurrentActionMap("Tank1");
        inputTurret1.SwitchCurrentActionMap("Turret1");

        if (devices.Length >= 2)
        {
            AssignDevice(inputTank1, devices[0]);
            AssignDevice(inputTurret1, devices[1]);

            Debug.Log($"Tank1 -> {devices[0].name}");
            Debug.Log($"Turret1 -> {devices[1].name}");
        }
        else
        {
            Debug.Log("Usando teclado para Tank1 y gamepad para Turret1");

            AssignDevice(inputTank1, Keyboard.current);

            if (devices.Length >= 1)
                AssignDevice(inputTurret1, devices[0]);
        }
    }

    private void SetupFourPlayers(Gamepad[] devices)
    {
        tank1.SetActive(true);
        tank2.SetActive(true);

        camTurret1.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
        camTank1.rect = new Rect(0f, 0f, 0.5f, 0.5f);
        camTurret2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        camTank2.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);

        camTank2.gameObject.SetActive(true);
        camTurret2.gameObject.SetActive(true);

        inputTank1.enabled = true;
        inputTurret1.enabled = true;
        inputTank2.enabled = true;
        inputTurret2.enabled = true;

        inputTank1.SwitchCurrentActionMap("Tank1");
        inputTurret1.SwitchCurrentActionMap("Turret1");
        inputTank2.SwitchCurrentActionMap("Tank2");
        inputTurret2.SwitchCurrentActionMap("Turret2");

        if (devices.Length >= 4)
        {
            AssignDevice(inputTank1, devices[0]);
            AssignDevice(inputTurret1, devices[1]);
            AssignDevice(inputTank2, devices[2]);
            AssignDevice(inputTurret2, devices[3]);
        }
        else
        {
            Debug.LogWarning("Menos de 4 mandos detectados. Se asignarán teclado y mandos mezclados.");

            AssignDevice(inputTank1, Keyboard.current);

            if (devices.Length > 0) AssignDevice(inputTurret1, devices[0]);
            if (devices.Length > 1) AssignDevice(inputTank2, devices[1]);
            if (devices.Length > 2) AssignDevice(inputTurret2, devices[2]);
        }
    }

    private void AssignDevice(PlayerInput playerInput, InputDevice device)
    {
        if (device == null) return;

        playerInput.user.UnpairDevices();
        InputUser.PerformPairingWithDevice(device, playerInput.user);

        Debug.Log($"Asignado {device.displayName} a {playerInput.gameObject.name}");
    }
}
