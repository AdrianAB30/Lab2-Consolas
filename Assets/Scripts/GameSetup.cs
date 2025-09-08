using UnityEngine;
using UnityEngine.InputSystem;

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
        if (GameManager.playerCount == 2)
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
            inputTank1.ActivateInput();
            inputTurret1.ActivateInput();

            Debug.Log("Current Map Tank1: " + inputTank1.currentActionMap?.name);
            Debug.Log("Current Map Turret1: " + inputTurret1.currentActionMap?.name);
        }
        else if (GameManager.playerCount == 4)
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

            inputTank1.ActivateInput();
            inputTurret1.ActivateInput();
            inputTank2.ActivateInput();
            inputTurret2.ActivateInput();

            Debug.Log("Current Map Tank1: " + inputTank1.currentActionMap?.name);
            Debug.Log("Current Map Turret1: " + inputTurret1.currentActionMap?.name);
            Debug.Log("Current Map Tank2: " + inputTank2.currentActionMap?.name);
            Debug.Log("Current Map Turret2: " + inputTurret2.currentActionMap?.name);
        }
    }
}
