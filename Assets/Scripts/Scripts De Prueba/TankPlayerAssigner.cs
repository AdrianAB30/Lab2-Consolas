using UnityEngine;
using UnityEngine.InputSystem;

public class TankPlayerAssigner : MonoBehaviour
{
    private TankMovement movement;
    private TurretControl turret;
    private int tankID; // 1 = tanque1, 2 = tanque2

    void Awake()
    {
        movement = GetComponentInChildren<TankMovement>();
        turret = GetComponentInChildren<TurretControl>();
    }

    public void SetTankID(int id)
    {
        tankID = id;
    }

    public void AssignPlayer(PlayerInput input, bool isDriver)
    {
        if (isDriver)
        {
            input.transform.SetParent(movement.transform, false);
            movement.enabled = true;
            turret.enabled = false;

            // 👉 Asignar Action Map específico para el tanque
            input.SwitchCurrentActionMap("Tank" + tankID);
            print(tankID);
        }
        else
        {
            input.transform.SetParent(turret.transform, false);
            movement.enabled = false;
            turret.enabled = true;

            // 👉 Asignar Action Map específico para el tanque
            input.SwitchCurrentActionMap("Turret" + tankID);
        }
    }
}
